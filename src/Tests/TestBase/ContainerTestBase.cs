﻿using Castle.MicroKernel;
using Castle.Windsor;
using NUnit.Framework;

namespace Orchard.Tests {
    public class ContainerTestBase {

       protected IWindsorContainer _container;

        [SetUp]
        public virtual void Init() {
            //var builder = new ContainerBuilder();
            _container = new WindsorContainer();

           //Register(builder);

            //_container = builder.Build();

            //Resolve(_container);
        }

#if false
        // technically more accurate, and doesn't work
        [SetUp]
        public virtual void Init() {
            var hostBuilder = new ContainerBuilder();
            var hostContainer = hostBuilder.Build();
            var shellContainer = hostContainer.BeginLifetimeScope("shell", shellBuilder => Register(shellBuilder));
            var workContainer = shellContainer.BeginLifetimeScope("work");

            _container = workContainer;
            Resolve(_container);
        }
#endif

        //protected virtual void Register(ContainerBuilder builder) { }
        //protected virtual void Resolve(ILifetimeScope container) { }
    }
}