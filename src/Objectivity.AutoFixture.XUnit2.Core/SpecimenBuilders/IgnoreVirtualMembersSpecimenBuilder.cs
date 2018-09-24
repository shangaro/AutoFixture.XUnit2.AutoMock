namespace Objectivity.AutoFixture.XUnit2.Core.SpecimenBuilders
{
    using System;
    using System.Reflection;
    using global::AutoFixture.Kernel;

    internal class IgnoreVirtualMembersSpecimenBuilder : ISpecimenBuilder
    {
        public IgnoreVirtualMembersSpecimenBuilder()
            : this(null)
        {
        }

        public IgnoreVirtualMembersSpecimenBuilder(Type reflectedType)
        {
            this.ReflectedType = reflectedType;
        }

        public Type ReflectedType { get; private set; }

        public object Create(object request, ISpecimenContext context)
        {
            var pi = request as PropertyInfo;
            if (pi != null) //// is a property
            {
                if (this.ReflectedType == null || //// is hosted anywhere
                    this.ReflectedType == pi.ReflectedType) //// is hosted in defined type
                {
                    if (pi.GetGetMethod().IsVirtual)
                    {
                        return new OmitSpecimen();
                    }
                }
            }

            return new NoSpecimen();
        }
    }
}