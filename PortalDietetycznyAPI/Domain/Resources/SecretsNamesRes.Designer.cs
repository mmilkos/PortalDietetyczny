﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PortalDietetycznyAPI.Domain.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class SecretsNamesRes {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal SecretsNamesRes() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("PortalDietetycznyAPI.Domain.Resources.SecretsNamesRes", typeof(SecretsNamesRes).Assembly);
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
        ///   Looks up a localized string similar to ApiKey.
        /// </summary>
        internal static string CloudinarySettings_ApiKey {
            get {
                return ResourceManager.GetString("CloudinarySettings_ApiKey", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ApiSecret.
        /// </summary>
        internal static string CloudinarySettings_ApiSecret {
            get {
                return ResourceManager.GetString("CloudinarySettings_ApiSecret", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to CloudName.
        /// </summary>
        internal static string CloudinarySettings_CloudName {
            get {
                return ResourceManager.GetString("CloudinarySettings_CloudName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to DropboxSettings_AppKey.
        /// </summary>
        internal static string DropboxSettings_AppKey {
            get {
                return ResourceManager.GetString("DropboxSettings_AppKey", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to DropboxSettings_AppSecret.
        /// </summary>
        internal static string DropboxSettings_AppSecret {
            get {
                return ResourceManager.GetString("DropboxSettings_AppSecret", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to DropboxSettings_RefreshToken.
        /// </summary>
        internal static string DropboxSettings_RefreshToken {
            get {
                return ResourceManager.GetString("DropboxSettings_RefreshToken", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to JwtExpireHours.
        /// </summary>
        internal static string PortalSettings_JwtExpireHours {
            get {
                return ResourceManager.GetString("PortalSettings_JwtExpireHours", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to JwtIssuer.
        /// </summary>
        internal static string PortalSettings_JwtIssuer {
            get {
                return ResourceManager.GetString("PortalSettings_JwtIssuer", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to JwtKey.
        /// </summary>
        internal static string PortalSettings_JwtKey {
            get {
                return ResourceManager.GetString("PortalSettings_JwtKey", resourceCulture);
            }
        }
    }
}
