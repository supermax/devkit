using System;
using DevKit.Core.Extensions;
using DevKit.Nexus.Binding.API;
using DevKit.Nexus.Binding.Internals;

namespace DevKit.Nexus.Binding
{
    public abstract class Binding : IBinding
    {
        public BindingMode Mode { get; }

        public object Source { get; protected set; }

        protected BindingPath SourceBindingPath { get; set; }

        public string SourcePath { get; }

        public object Target { get; protected set; }

        protected BindingPath TargetBindingPath { get; set; }

        public string TargetPath { get; }

        protected Binding(object source, string sourcePath, object target, string targetPath, BindingMode mode)
        {
            source.ThrowIfNull(nameof(source));
            target.ThrowIfNull(nameof(target));

            sourcePath.ThrowIfNullOrEmpty(nameof(sourcePath));
            targetPath.ThrowIfNullOrEmpty(nameof(targetPath));

            // TODO move to Bind() method
            // var sourceType = source.GetType();
            // var targetType = target.GetType();
            // if (sourceType != targetType)
            // {
            //     throw new OperationCanceledException($"{nameof(source)} type ({sourceType}) " +
            //                                          $"is not same as {nameof(target)} type ({targetType})");
            // }

            Source = source;
            SourcePath = sourcePath;

            Target = target;
            TargetPath = targetPath;

            Mode = mode;
        }

        public IBinding SetSourceBindingPath(BindingPath path)
        {
            SourceBindingPath = path;
            return this;
        }

        public IBinding SetTargetBindingPath(BindingPath path)
        {
            TargetBindingPath = path;
            return this;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }
            Source = null;
            Target = null;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public override string ToString()
        {
            var mode = Mode switch
                {
                    BindingMode.OneWay => "=>",
                    BindingMode.TwoWay => "<=>",
                    BindingMode.OneTime => "=1",
                    _ => ":"
                };
            return $"{Source}.{SourcePath} {mode} {Target}{TargetPath}";
        }

        public abstract IBinding InitValues();

        public abstract IBinding Bind();
    }
}
