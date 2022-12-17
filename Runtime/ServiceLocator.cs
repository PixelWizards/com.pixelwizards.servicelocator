using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PixelWizards.Shared.Base
{
    /// <summary>
    ///     Our primary service location pattern.All global services are registered with us
    ///     
    /// Services can be normal monobehaviours, just add a static 'Instance' method like so:
    /// 
    ///     public class MyNewService : MonoBehaviour
    ///     {
    ///         public static MyNewService Instance
    ///         {
    ///             get { return ServiceLocator.Get<MyNewService>(); }
    ///         }
    ///     }
    ///
    /// Then you can add them to the locator using Add<T> like so:
    /// Add a monobehaviour service to the locator
    /// var myService = go.GetOrAddComponent<MyNewService>();
    /// ServiceLocator.Add(myService);
    ///
    /// or find it in the scene, and then add that:
    ///
    /// var myService = (MyNewService)FindObjectOfType(typeof(MyNewService));
    /// if (myService == null) { myService = go.GetOrAddComponent<MyNewService>(); }
    /// ServiceLocator.Add(myService);
    ///
    /// The same goes for normal C# classes (non monobehaviour services), just add a static Instance and then add them using AddRaw<T>()
    /// 
    /// public class GameSettings
    /// {
    ///     public static GameSettings Instance
    ///     {
    ///         get { return ServiceLocator.Get<GameSettings>(); }
    ///     }
    /// }
    ///
    /// var myService = new GameSettings();
    /// ServiceLocator.AddRaw(myService);
    /// </ summary >
    public class ServiceLocator
    {
        private static readonly List<object> services = new List<object>();

        public static List<object> Services {  get { return services; } }

        /// <summary>
        /// Get a handle to the active instance of the requested service type
        /// </summary>
        /// <typeparam name="T">Type of the service that we are looking for</typeparam>
        /// <returns>handle to the instance if it exists, 'default' (likely null) if not</returns>
        public static T Get<T>()
        {
            var matching = services.OfType<T>().ToList();
            if (matching.Count < 1)
            {
                Debug.Log("Failed to get service of type: " + typeof(T).FullName);
                return default;
            }
            else
            {
                return matching[0];
            }
        }

        /// <summary>
        /// Check if we have a service already of this type
        /// </summary>
        /// <typeparam name="T">Type of the service that we are looking for</typeparam>
        /// <returns>true if the service has been registered (we have a valid instance), false if not</returns>
        public static bool Has<T>()
        {
            var matching = services.OfType<T>().ToList();
            return matching.Count > 0;
        }

        /// <summary>
        /// Add a monobehaviour service to the locator
        /// var myService = go.GetOrAddComponent<MyService>();
        /// ServiceLocator.Add(myService);
        /// 
        /// or find it in the scene, and then add that:
        /// 
        /// var myService = (MyService)FindObjectOfType(typeof(MyService));
        /// if (myService == null) { myService = go.GetOrAddComponent<MyService>(); }
        /// ServiceLocator.Add(myService);
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="service">Instance of the service that we want to add</param>
        /// <returns>Handle to the service</returns>
        public static T Add<T>(T service) where T : MonoBehaviour
        {
            //Object.DontDestroyOnLoad(service.gameObject);
            services.Add(service);
            return service;
        }

        /// <summary>
        /// Add a raw (non Monobehaviour service) to the locator, for example:
        /// var myService = new MyService();
        /// ServiceLocator.AddRaw(myService);
        /// </summary>
        /// <typeparam name="T">Instance of the service to add</typeparam>
        /// <param name="service">Handle to the service</param>
        /// <returns></returns>
        public static T AddRaw<T>(T service)
        {
            services.Add(service);
            return service;
        }

        /// <summary>
        /// In case we need to remove any services
        /// </summary>
        public static bool Remove<T>(T service) where T : MonoBehaviour
        {
            if (services.Contains(service))
            {
                services.Remove(service);
                return true;
            }
            return false;
        }

        public static bool RemoveRaw<T>(T service)
        {
            if( services.Contains( service))
            {
                services.Remove(service);
                return true;
            }
            return false;
        }

    }
}