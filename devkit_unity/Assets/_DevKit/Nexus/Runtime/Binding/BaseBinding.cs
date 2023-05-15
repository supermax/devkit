using System;
using DevKit.Core.Extensions;
using DevKit.Nexus.Binding.API;

namespace DevKit.Nexus.Binding
{
    public abstract class Binding : IDisposable
    {
        public BindingMode Mode { get; }

        public object Source { get; protected set; }

        public string SourcePath { get; }

        public object Target { get; protected set; }

        public string TargetPath { get; }

        protected Binding(object source, string sourcePath, object target, string targetPath, BindingMode mode)
        {
            source.ThrowIfNull(nameof(source));
            target.ThrowIfNull(nameof(target));

            sourcePath.ThrowIfNullOrEmpty(nameof(sourcePath));
            targetPath.ThrowIfNullOrEmpty(nameof(targetPath));

            var sourceType = source.GetType();
            var targetType = target.GetType();
            if (sourceType != targetType)
            {
                throw new OperationCanceledException($"{nameof(source)} type ({sourceType}) " +
                                                     $"is not same as {nameof(target)} type ({targetType})");
            }

            Source = source;
            SourcePath = sourcePath;

            Target = target;
            TargetPath = targetPath;

            Mode = mode;
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
    }
}
