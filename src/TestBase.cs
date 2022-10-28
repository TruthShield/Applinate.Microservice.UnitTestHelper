﻿// Copyright (c) TruthShield, LLC. All rights reserved.

namespace Applinate.Test
{
    /// <summary>
    /// Class TestBase.
    /// </summary>
    public class TestBase
    {
        public TestBase(ServiceType serviceType = ServiceType.Client, bool useEncryption = true)
        {
            Initialize(serviceType);
        }

        [STAThread]
        private static void Initialize(ServiceType serviceType)
        {
            TestHelper.OnlyLoadReferencedAssemblies();

            InitializationProvider.Initialize(true);

            TestHelper.SetRequestContext(serviceType);

            ServiceProvider.RegisterSingleton<IRequestHandler, DefaultRequestExecutor>();
        }
    }
}