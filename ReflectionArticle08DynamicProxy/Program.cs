using Castle.DynamicProxy;

namespace ReflectionArticle08DynamicProxy
{
    /// <summary>
    /// Interface that defines the target service methods.
    /// </summary>
    public interface ITargetService
    {
        void PerformAction();
    }

    /// <summary>
    /// The actual implementation of the target service.
    /// </summary>
    public class TargetService : ITargetService
    {
        /// <summary>
        /// Implements the PerformAction method.
        /// </summary>
        public void PerformAction()
        {
            Console.WriteLine("Target service is performing an action.");
        }
    }

    /// <summary>
    /// An interceptor for logging method calls.
    /// </summary>
    public class LoggingInterceptor : IInterceptor
    {
        /// <summary>
        /// Intercepts method calls and adds logging before and after execution.
        /// </summary>
        /// <param name="invocation">The invocation information.</param>
        public void Intercept(IInvocation invocation)
        {
            // Log the method call before executing it.
            Console.WriteLine($"Calling method: {invocation.Method.Name}");

            // Execute the original method.
            invocation.Proceed();

            // Log the completion of the method.
            Console.WriteLine($"Method {invocation.Method.Name} completed.");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Create a proxy generator instance.
            var proxyGenerator = new ProxyGenerator();

            // Create a dynamic proxy for the ITargetService interface with the LoggingInterceptor.
            ITargetService proxy = proxyGenerator.CreateInterfaceProxyWithTarget(
                typeof(ITargetService), new TargetService(), new LoggingInterceptor()) as ITargetService;

            // Use the proxy as if it were the original object.
            proxy.PerformAction();

            Console.ReadKey();
        }
    }
}
