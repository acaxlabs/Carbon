using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carbon.Ioc
{
    /// <summary>
    /// Provides a basic inversion of control with settings in the app.config. 
    /// </summary>
    public class Container
    {
        static string configKey = "carbon.ioc-"; //prefix in app.config file for registers
        static Dictionary<Type, Object> instances = new Dictionary<Type, object>();
        static bool initalized = false;

        /// <summary>
        /// Get an instance from the ioc container. 
        /// </summary>
        /// <typeparam name="T">The type of instance</typeparam>
        /// <returns>an instance</returns>
        public static T Get<T>()
        {
            if (!initalized) Initialize();
            Type type = typeof(T);
            if (instances.ContainsKey(type)) return (T)instances[type];
            throw new Exception($"No {type.FullName} is in the Carbon.Ioc container");
        }

        /// <summary>
        /// Add an instance to the container
        /// </summary>
        /// <param name="type">The instance type</param>
        /// <param name="value">The instance</param>
        public static void Register(Type type, Object value)
        {
            instances.Add(type, value);
        }

        /// <summary>
        /// Initializes the Ioc container. This will find all of the registered types and instantiate them.
        /// </summary>
        private static void Initialize()
        {
            System.Collections.Specialized.NameValueCollection settings = ConfigurationManager.AppSettings;
            foreach (string key in settings.AllKeys)
            {
                if (!key.ToLower().StartsWith(configKey)) continue;
                RegisterFromSetting(key, settings[key]);
            }
            initalized = true;
        }

        /// <summary>
        /// creates a type from a key value pair and registers it in the ioc
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        private static void RegisterFromSetting(string key, string value)
        {
            try
            {
                string typeName = key.Substring(configKey.Length);
                Type interfaceType = Type.GetType(typeName);
                if (interfaceType == null) throw new Exception($"{interfaceType} was not found. Use the format \"namespace.namespace.classname,assemblyname\"");
                Type instanceType = Type.GetType(value);
                if (instanceType == null) throw new Exception($"{instanceType} was not found. Use the format \"namespace.namespace.classname,assemblyname\"");
                if (instances.ContainsKey(interfaceType)) return; 
                Object obj = Activator.CreateInstance(instanceType);
                instances.Add(interfaceType, obj);
            }
            catch (Exception ex)
            {
                throw new Exception($"Type {value} could not be instatiated for {key}. See inner exception.", ex);
            }
        }
    }
}