# com.pixelwizards.servicelocator

Our primary service location pattern. All global services are registered with this.

For more information on what a Service Locator is or does, check these links:

https://stackify.com/service-locator-pattern/

https://www.geeksforgeeks.org/service-locator-pattern/

Services can be normal MonoBehaviours, just add a static 'Instance' method like so:

`public class MyNewService : MonoBehaviour`
`{`
	`public static MyNewService Instance`
    `{`
		`get { return ServiceLocator.Get<MyNewService>(); }`
    `}`
`}`

Then you can add them to the locator using Add<T> like so:

Add a MonoBehaviour service to the locator:

`var myService = go.GetComponent<MyNewService>();`
`ServiceLocator.Add(myService);`

 or find it in the scene, and then add that:

`var myService = (MyNewService)FindObjectOfType(typeof(MyNewService));`
`if (myService == null) { myService = go.GetComponent<MyNewService>(); }`
`ServiceLocator.Add(myService);`

The same goes for POCO / normal C# classes (non MonoBehaviour services), just add a static Instance and then add them using AddRaw<T>()

`public class GameSettings`
`{`
	`public static GameSettings Instance`
    `{`
		`get { return ServiceLocator.Get<GameSettings>(); }`
	`}`
`}`

 `var myService = new GameSettings();`
 `ServiceLocator.AddRaw(myService);`

Once you have registered the service with the locator, you can reference it from anywhere via the Instance method, like so:

`MyService.Instance.DoSomething();`    