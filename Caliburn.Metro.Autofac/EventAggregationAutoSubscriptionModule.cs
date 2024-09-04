using Caliburn.Micro;
using Autofac;
using Autofac.Core;
using Autofac.Core.Registration;
using Autofac.Core.Resolving.Pipeline;
using System.Runtime.InteropServices;

namespace Caliburn.Metro.Autofac
{
    public class EventAggregationAutoSubscriptionModule : Module
    {
       protected override void AttachToComponentRegistration(IComponentRegistryBuilder componentRegistry, IComponentRegistration registration)
        {
            registration.PipelineBuilding += OnComponentActivated;
        }

       static void OnComponentActivated(object sender, IResolvePipelineBuilder pipeline)
       {
           pipeline.Use(PipelinePhase.Activation, MiddlewareInsertionMode.EndOfPhase, (c, next) =>
           {
               next(c);

               //  we never want to fail, so check for null (should never happen), and return if it is
               if (c == null)
                   return;

               //  try to convert instance to IHandle
               //  I originally did e.Instance.GetType().IsAssignableTo<>() and then 'as', 
               //  but it seemed redundant
               var handler = c.Instance;
               
               //  if it is not null, it implements, so subscribe
               if (handler != null)
                   c.DecoratorContext?.Resolve<IEventAggregator>().SubscribeOnPublishedThread(handler);

           });
        }
    }
}
