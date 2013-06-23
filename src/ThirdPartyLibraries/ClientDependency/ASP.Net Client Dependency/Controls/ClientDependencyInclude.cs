﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;

namespace ClientDependency.Core.Controls
{
    public abstract class ClientDependencyInclude : Control, IClientDependencyFile
	{

		public ClientDependencyInclude()
		{			
            Priority = DefaultPriority;			
			PathNameAlias = "";
		}

		public ClientDependencyInclude(IClientDependencyFile file)
		{
			Priority = file.Priority;
			PathNameAlias = file.PathNameAlias;
			FilePath = file.FilePath;
			DependencyType = file.DependencyType;
		}

        /// <summary>
        /// If a priority is not set, the default will be 100.
        /// </summary>
        /// <remarks>
        /// This will generally mean that if a developer doesn't specify a priority it will come after all other dependencies that 
        /// have unless the priority is explicitly set above 100.
        /// </remarks>
        public const int DefaultPriority = 100;

		public ClientDependencyType DependencyType { get; internal set; }

		public string FilePath { get; set; }
        public string PathNameAlias { get; set; }
        public int Priority { get; set; }

		/// <summary>
		/// This can be empty and will use default provider
		/// </summary>
		public string ForceProvider { get; set; }

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (string.IsNullOrEmpty(FilePath))
				throw new NullReferenceException("Both File and Type properties must be set");
		}

	}
}
