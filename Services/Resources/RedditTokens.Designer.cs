﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlockBase.Dapps.NeverForget.Services.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class RedditTokens {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal RedditTokens() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("BlockBase.Dapps.NeverForget.Services.Resources.RedditTokens", typeof(RedditTokens).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to a4KguMvy27__0w.
        /// </summary>
        internal static string APP_ID {
            get {
                return ResourceManager.GetString("APP_ID", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to x+&amp;cX5MM6qCjB+s.
        /// </summary>
        internal static string PASSWORD {
            get {
                return ResourceManager.GetString("PASSWORD", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to http://localhost:8080/Reddit.NET/oauthRedirect.
        /// </summary>
        internal static string REDIRECT_URI {
            get {
                return ResourceManager.GetString("REDIRECT_URI", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 1y8ER7jIOM-WwJSKGBdk8YdiamlMCw.
        /// </summary>
        internal static string SECRET {
            get {
                return ResourceManager.GetString("SECRET", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to NeverForgetBot1.0.
        /// </summary>
        internal static string USER_AGENT {
            get {
                return ResourceManager.GetString("USER_AGENT", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to NeverForget-Bot.
        /// </summary>
        internal static string USERNAME {
            get {
                return ResourceManager.GetString("USERNAME", resourceCulture);
            }
        }
    }
}