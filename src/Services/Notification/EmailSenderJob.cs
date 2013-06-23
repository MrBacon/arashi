﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arashi.Services.Notification
{
   using Arashi.Core;
   using Arashi.Core.Domain;
   using Arashi.Core.Repositories;

   using Castle.Components.Scheduler;

   using log4net;

   using NHibernate;
   using NHibernate.Criterion;

   /// <summary>
   /// Job to send emails with Castle Scheduler
   /// </summary>
   public class EmailSenderJob : IJob, IDisposable
   {
      private static readonly ILog log = LogManager.GetLogger(typeof(EmailSenderJob));
      private int maxEmailsToSend = 5;

      #region Constructor


      public EmailSenderJob()
      {
      }

      #endregion


      /// <summary>
      /// 	Executes the job.
      /// </summary>
      /// <param name = "context">The job's execution context</param>
      /// <returns>
      /// 	True if the job succeeded, false otherwise
      /// </returns>
      /// <exception cref = "T:System.Exception">Any exception thrown by the job is interpreted
      /// 	as an error by the scheduler.  Changes made to the job's state data
      /// 	will be discarded when this occurs.</exception>
      public bool Execute(JobExecutionContext context)
      {
         log.Debug("EmailSenderJob.Execute: Start");

         IList<Message> messages = GetMessagesToSend();
         log.DebugFormat("EmailSenderJob.Execute: Number of messages to process: {0}", messages.Count.ToString() );

         EmailService emailService = new EmailService();

         foreach (Message message in messages)
         {
            try
            {
               // sending...
               message.Status = MessageStatus.Sending;
               message.UpdatedDate = DateTime.UtcNow;
               //messageService.Save(message);
               this.SaveMessage(message);
               log.DebugFormat("EmailSenderJob.Execute: Sending message with Id {0}", message.MessageId.ToString());

               // send the message
               emailService.Send(message);

               // sent!
               message.Status = MessageStatus.Sent;
               message.UpdatedDate = DateTime.UtcNow;
               //messageService.Save(message);
               this.SaveMessage(message);
               log.DebugFormat("EmailSenderJob.Execute: message with Id {0} is sent!", message.MessageId.ToString());
            }
            catch (Exception ex)
            {
               log.ErrorFormat("EmailSenderJob.Execute: Exception while processing message with Id {0}.\r\n{1}", message.MessageId.ToString(), ex.ToString());
               message.Status = MessageStatus.Queued;
               message.UpdatedDate = DateTime.UtcNow;
               message.AttemptsCount++;

               if (message.AttemptsCount >= 3)
               {
                  message.AttemptsCount = 3;
                  message.Status = MessageStatus.NotSent;
               }

               //messageService.Save(message);
               this.SaveMessage(message);
            }
         } // end foreach

         log.Debug("EmailSenderJob.Execute: End");
         return true;
      }


      public void Dispose()
      {
      }




      private IList<Message> GetMessagesToSend()
      {
         //When performing things in a separate thread, you have to obtain a session manually because as if you have noticed, 
         // there is no HttpContext where the session manager retrieves its session from (don't remove isWeb from the config).

         ISessionFactory sessionFactory = IoC.Resolve<ISessionFactory>();
         using (ISession session = sessionFactory.OpenSession())
         {
            IList<Message> messages = session.GetNamedQuery("GetQueuedEmailMessages").SetMaxResults(maxEmailsToSend)
                                         .List<Message>();

            return messages;
         }
      }




      private void SaveMessage(Message message)
      {
         //When performing things in a separate thread, you have to obtain a session manually because as if you have noticed, 
         // there is no HttpContext where the session manager retrieves its session from (don't remove isWeb from the config).

         ISessionFactory sessionFactory = IoC.Resolve<ISessionFactory>();
         using (ISession session = sessionFactory.OpenSession())
         {
            using (ITransaction tx = session.BeginTransaction())
            {
               session.SaveOrUpdate(message);
               tx.Commit();
            }
         }
      }



      //private void ConfigureEmailSender()
      //{
      //   INeedSmtpConfiguration service = emailSender as INeedSmtpConfiguration;
      //   if (service != null)
      //   {
      //      logger.Debug("Configuring email service");
      //      SiteConfigurationDTO configuration = configurationService.GetConfiguration();
      //      service.Configure(configuration.SmtpConfiguration);
      //      logger.Debug("Email service has been configured");
      //   }
      //}

   }
}
