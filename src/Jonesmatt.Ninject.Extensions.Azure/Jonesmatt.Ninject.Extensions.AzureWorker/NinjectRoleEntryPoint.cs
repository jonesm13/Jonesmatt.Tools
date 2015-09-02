using Microsoft.WindowsAzure.ServiceRuntime;
using Ninject;

namespace Jonesmatt.Ninject.Extensions.AzureWorker
{
    public abstract class NinjectRoleEntryPoint : RoleEntryPoint
    {
        protected IKernel Kernel { get; set; }

        public sealed override bool OnStart()
        {
            Kernel = CreateKernel();
            Kernel.Inject(this);
            return OnRoleStarted();
        }

        public abstract override void Run();

        public sealed override void OnStop()
        {
            OnRoleStopping();

            if (Kernel != null)
            {
                Kernel.Dispose();
                Kernel = null;
            }

            OnRoleStopped();
        }

        protected abstract IKernel CreateKernel();

        protected virtual bool OnRoleStarted()
        {
            return true;
        }

        protected virtual void OnRoleStopping()
        {
        }

        protected virtual void OnRoleStopped()
        {
        }
    }
}
