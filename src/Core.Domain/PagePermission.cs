using System;

namespace Arashi.Core.Domain
{
	/// <summary>
	/// Association class between Node and Role.
	/// </summary>
   public class PagePermission : IPermission
	{
      #region Private Fields

      private int id;
      private bool viewAllowed;
      private bool editAllowed;
      private bool deleteAllowed;
      private Role role;
      private Page page;

      #endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
      public PagePermission()
		{
         this.id = -1;
         this.viewAllowed = false;
         this.editAllowed = false;
         this.deleteAllowed = false;
      }


      #region Public Properties
      
      /// <summary>
      /// Property Id (int)
      /// </summary>
      public virtual int Id
      {
         get
         {
            return this.id;
         }
         set
         {
            this.id = value;
         }
      }

      /// <summary>
      /// Property ViewAllowed
      /// </summary>
      public virtual bool ViewAllowed
      {
         get
         {
            return this.viewAllowed;
         }
         set
         {
            this.viewAllowed = value;
         }
      }

      /// <summary>
      /// Property EditAllowed
      /// </summary>
      public virtual bool EditAllowed
      {
         get
         {
            return this.editAllowed;
         }
         set
         {
            this.editAllowed = value;
         }
      }

      public virtual bool DeleteAllowed
      {
         get
         {
            return deleteAllowed;
         }
         set
         {
            deleteAllowed = value;
         }
      }

      /// <summary>
      /// Property Role (Role)
      /// </summary>
      public virtual Role Role
      {
         get
         {
            return this.role;
         }
         set
         {
            this.role = value;
         }
      }

		/// <summary>
		/// Property Page (Page)
		/// </summary>
      public virtual Page Page
		{
			get { return page; }
			set { page = value; }
      }

      #endregion

      #region Equality

      public override bool Equals(object other)
      {
         if (this == other)
            return true;
         PagePermission permission = other as PagePermission;
         if (permission == null)
            return false;
         if (Id != permission.Id)
            return false;
         return true;
      }



      public override int GetHashCode()
      {
         unchecked
         {
            int result;
            result = Id.GetHashCode();
            result = 29 * result;
            return result;
         }
      }

      #endregion

   }
}
