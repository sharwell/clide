﻿#region BSD License
/* 
Copyright (c) 2012, Clarius Consulting
All rights reserved.

Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

* Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.

* Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.

* Neither the name of Clarius Consulting nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/
#endregion

namespace Clide.Commands
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.Composition;

    /// <summary>
    /// Attribute that must be placed on command implementations in order to 
    /// use the <see cref="ICommandManager.AddCommands"/> method.
    /// </summary>
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class CommandFilterAttribute : ComponentAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandFilterAttribute"/> class 
        /// from a dictionary of values
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public CommandFilterAttribute(IDictionary<string, object> attributes)
            : this((string)attributes["PackageId"], (string)attributes["GroupId"], (int)attributes["CommandId"])
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandFilterAttribute" /> class.
        /// </summary>
        /// <param name="packageGuid">Identifer for the package that owns/exposes the given command.</param>
        /// <param name="groupGuid">Gets the command group GUID (also known as CommandSet ID).</param>
        /// <param name="commandId">The command id.</param>
        public CommandFilterAttribute(string packageGuid, string groupGuid, int commandId)
            : base(typeof(ICommandFilter))
        {
            this.PackageId = packageGuid;
            this.GroupId = groupGuid;
            this.CommandId = commandId;
        }

        /// <summary>
        /// Identifer for the package that owns/exposes the given command.
        /// </summary>
        public string PackageId { get; private set; }

        /// <summary>
        /// Gets the command group GUID (also known as CommandSet ID).
        /// </summary>
        public string GroupId { get; private set; }

        /// <summary>
        /// Gets the command id.
        /// </summary>
        public int CommandId { get; private set; }
    }
}
