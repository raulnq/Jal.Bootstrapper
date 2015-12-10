# Jal.Bootstrapper
Just another library to setup a group of classes
##How to use?
**Create your Bootstrapper class**

    public class DoSomethingBootstrapper : IBootstrapper<bool>
    {
        public void Configure()
        {
            Result = true;
        }
        public bool Result { get; private set; }
    }
	
**Create an instance of your class and add it to the CompositeBootstrapper class**

	var bootstrapper = new DoSomethingBootstrapper();
	var composite = new CompositeBootstrapper(new IBootstrapper[] { bootstrapper });
	
**Call the Configure method of the CompositeBootstrapper class**

	composite.Configure();
	
**Check the results of your Bootstrapper class looking the property Result**
