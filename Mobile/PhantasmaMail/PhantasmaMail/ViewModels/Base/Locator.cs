﻿using Autofac;
using PhantasmaMail.Services.Dialog;
using PhantasmaMail.Services.Navigation;
using System;
using NeoModules.Rest.Services;
using PhantasmaMail.Services;
using PhantasmaMail.Services.Authentication;
using PhantasmaMail.Services.Db;
using PhantasmaMail.Services.Phantasma;

namespace PhantasmaMail.ViewModels.Base
{
    public class Locator
    {
        private IContainer _container;
        private readonly ContainerBuilder _containerBuilder;

        private static readonly Locator _instance = new Locator();

        public static Locator Instance => _instance;

        public Locator()
        {
            _containerBuilder = new ContainerBuilder();

            _containerBuilder.RegisterType<NavigationService>().SingleInstance().As<INavigationService>();
            _containerBuilder.RegisterType<DialogService>().SingleInstance().As<IDialogService>();
            _containerBuilder.RegisterType<AuthenticationService>().SingleInstance().As<IAuthenticationService>();
            _containerBuilder.RegisterType<WalletService>().SingleInstance();
            _containerBuilder.RegisterType<PhantasmaService>().SingleInstance().As<IPhantasmaService>();
            //_containerBuilder.RegisterType<NeoScanRestService>().SingleInstance().As<INeoRestService>();

            _containerBuilder.RegisterType<ExtendedSplashViewModel>();
            _containerBuilder.RegisterType<LoginViewModel>();
            _containerBuilder.RegisterType<MainViewModel>();
            _containerBuilder.RegisterType<MenuViewModel>();
            _containerBuilder.RegisterType<InboxViewModel>();
            _containerBuilder.RegisterType<SentViewModel>();
            _containerBuilder.RegisterType<DraftsViewModel>().SingleInstance();
            _containerBuilder.RegisterType<ComposeViewModel>();
            _containerBuilder.RegisterType<MessageDetailViewModel>();
            _containerBuilder.RegisterType<RegisterBoxViewModel>();
            _containerBuilder.RegisterType<WalletTabViewModel>();
            _containerBuilder.RegisterType<SettingsViewModel>();
            _containerBuilder.RegisterType<ChangeServerViewModel>();

            //db
            _containerBuilder.RegisterType<PhantasmaDb>().SingleInstance().As<IPhantasmaDb>();
        }

        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        public object Resolve(Type type)
        {
            return _container.Resolve(type);
        }

        public void Register<TInterface, TImplementation>() where TImplementation : TInterface
        {
            _containerBuilder.RegisterType<TImplementation>().As<TInterface>();
        }

        public void Register<T>() where T : class
        {
            _containerBuilder.RegisterType<T>();
        }

        public void Build()
        {
            if (_container != null) return;
            _container = _containerBuilder.Build();
        }
    }
}