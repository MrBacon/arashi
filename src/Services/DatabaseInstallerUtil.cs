﻿namespace Arashi.Services
{
   using System;

   using System.Data;
   using System.IO;
   using System.Text.RegularExpressions;
   using Arashi.Core;
   using Arashi.Core.Domain;
   using Arashi.Services.SiteStructure;
   using Common.Logging;
   using NHibernate;
   using NHibernate.Dialect;
   using NHibernate.Engine;



   /// <summary>
   /// The DataBaseUtil class contains helper methods for misc. database related tasks.
   /// </summary>
   public class DatabaseUtil
   {
      private static ILog log = LogManager.GetCurrentClassLogger();

      private DatabaseUtil()
      {
      }



      /// <summary>
      /// Get the current database type.
      /// </summary>
      /// <returns></returns>
      public static DatabaseType GetCurrentDatabaseType()
      {
         //ISessionFactoryImplementor nhSessionFactory = GetNHibernateSessionFactory();
         Arashi.Core.NHibernate.ISessionFactory sf = GetSessionFactory();
         ISessionFactoryImplementor nhSessionFactory = sf.GetSession().SessionFactory as ISessionFactoryImplementor;

         if (nhSessionFactory.Dialect is MsSql2000Dialect ||
             nhSessionFactory.Dialect is MsSql2005Dialect ||
             nhSessionFactory.Dialect is MsSql2008Dialect)
            return DatabaseType.MsSql;
         else if (nhSessionFactory.Dialect is PostgreSQLDialect)
            return DatabaseType.PostgreSQL;
         else if (nhSessionFactory.Dialect is MySQLDialect)
            return DatabaseType.MySQL;

         throw new Exception("Unknown database type configured.");
      }



      /// <summary>
      /// Check if the database connection string in the web.config (NHibernate configuration) is valid.
      /// </summary>
      /// <returns></returns>
      public static bool TestDatabaseConnection()
      {
         Arashi.Core.NHibernate.ISessionFactory sf = GetSessionFactory();

         try
         {
            ISession session = sf.GetSession();
            IDbConnection cnn = session.Connection;

            if (cnn.State == ConnectionState.Open && session.IsConnected && session.IsOpen)
               return true;

            return false;
         }
         catch (Exception ex)
         {
            log.Error("Unable to connect to database.", ex);
            return false;
         }
      }



      /// <summary>
      /// Execute a given SQL script file.
      /// </summary>
      /// <param name="scriptFilePath"></param>
      public static void ExecuteSqlScript(string scriptFilePath)
      {
         log.Info("Executing script: " + scriptFilePath);

         string delimiter = GetDelimiter();
         StreamReader scriptFileStreamReader = new StreamReader(scriptFilePath);
         string completeScript = scriptFileStreamReader.ReadToEnd();

         ISession s = IoC.Resolve<Arashi.Core.NHibernate.ISessionFactory>().GetSession();

         IDbConnection connection = s.Connection; // nhSessionFactory.ConnectionProvider.GetConnection());
         IDbTransaction transaction = connection.BeginTransaction();
         
         try
         {
            IDbCommand cmd = connection.CreateCommand();
            cmd.Transaction = transaction;
            string splitRegex = delimiter + @"\s*\n";
            string[] sqlCommands = Regex.Split(completeScript, splitRegex, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            
            // Strip the delimiter from the last command. It might not be caught by the regex.
            sqlCommands[sqlCommands.Length - 1] = Regex.Replace(sqlCommands[sqlCommands.Length - 1], delimiter, String.Empty, RegexOptions.IgnoreCase | RegexOptions.Multiline);

            foreach (string sqlCommand in sqlCommands)
            {
               if (sqlCommand.Trim().Length > 0)
               {
                  log.Info("Executing the follwing command: " + sqlCommand);
                  cmd.CommandText = sqlCommand;
                  cmd.ExecuteNonQuery();
               }
            }

            log.Info("Committing transaction for script: " + scriptFilePath);
            transaction.Commit();
         }
         catch (Exception ex)
         {
            log.Warn("Rolling back transaction for script: " + scriptFilePath);
            log.Error("An error occured while executing the following script: " + scriptFilePath, ex);
            transaction.Rollback();
            throw new Exception("An error occured while executing the following script: " + scriptFilePath, ex);
         }
         finally
         {
            connection.Close();
            scriptFileStreamReader.Close();
         }
      }



      /// <summary>
      /// Get the version of the given assembly from the database.
      /// </summary>
      /// <param name="assembly"></param>
      /// <returns></returns>
      public static System.Version GetAssemblyVersion(string assembly)
      {
         System.Version version = null;

         try
         {
            IVersionService versionService = IoC.Resolve<IVersionService>();
            Arashi.Core.Domain.Version v = versionService.GetVersionForAssembly(assembly);
            
            // if the given assembly is not present in the Version table, return the lowest version number 0.0.0
            version = v != null ? new System.Version(v.Major, v.Minor, v.Patch) 
                        : new System.Version(0, 0, 0);
         }
         catch (Exception ex)
         {
            log.Error(String.Format("An error occured while retrieving the version for {0}.", assembly), ex);
            throw;
         }

         return version;
      }



      /// <summary>
      /// Get the SessionFactoryImplementor via its name (see Arashi.Web/Config/facilities.config) instead of
      /// its type because the NHibernateIntegration facility allows for more than one session factories
      /// </summary>
      /// <returns></returns>
      private static NHibernate.ISessionFactory GetNHibernateSessionFactory()
      {
         return IoC.Resolve<Arashi.Core.NHibernate.ISessionFactory>().GetSession().SessionFactory;
      }


      private static Arashi.Core.NHibernate.ISessionFactory GetSessionFactory()
      {
         return IoC.Resolve<Arashi.Core.NHibernate.ISessionFactory>();
      }


      private static string GetDelimiter()
      {
         switch (GetCurrentDatabaseType())
         {
            case DatabaseType.MsSql:
               return "^go";
            case DatabaseType.PostgreSQL:
            case DatabaseType.MySQL:
               return ";";
            default:
               throw new Exception("Unknown database type.");
         }
      }

   }
}