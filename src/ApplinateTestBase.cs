// Copyright (c) TruthShield, LLC. All rights reserved.

namespace Applinate.Test
{
    using Applinate.Internals;

    /// <summary>
    /// Class TestBase.
    /// </summary>
    public class ApplinateTestBase
    {
        public ApplinateTestBase(ServiceType serviceType = ServiceType.Client, bool useEncryption = true)
        {
            Initialize(serviceType);
        }

        [STAThread]
        private static void Initialize(ServiceType serviceType)
        {
            ApplinateTestHelper.OnlyLoadReferencedAssemblies();

            InitializationProvider.Initialize(true);

            ApplinateTestHelper.SetRequestContext(serviceType);

            RequestExecutorHelper.Register();
        }
    }
}