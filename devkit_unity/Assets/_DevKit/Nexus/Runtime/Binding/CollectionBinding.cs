using System;
using System.Collections;
using DevKit.Core.Extensions;
using DevKit.Core.Observables.API;
using DevKit.Nexus.Binding.API;
using DevKit.Nexus.Binding.Internals;

namespace DevKit.Nexus.Binding
{
    public class CollectionBinding : Binding
    {
        private CollectionBindingPath SourceBindingPath { get; set; }

        private CollectionBindingPath TargetBindingPath { get; set; }

        internal CollectionBinding(
            object source
            , string sourcePath
            , object target
            , string targetPath
            , BindingMode mode)
            : base(source, sourcePath, target, targetPath, mode)
        {

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (SourceBindingPath != null)
                {
                    SourceBindingPath?.Dispose();
                    SourceBindingPath = null;
                }
                if (TargetBindingPath != null)
                {
                    TargetBindingPath?.Dispose();
                    TargetBindingPath = null;
                }
            }
            base.Dispose(disposing);
        }

        internal CollectionBinding SetSourceBindingPath(CollectionBindingPath path)
        {
            SourceBindingPath = path;
            return this;
        }

        internal CollectionBinding SetTargetBindingPath(CollectionBindingPath path)
        {
            TargetBindingPath = path;
            return this;
        }

        internal CollectionBinding InitValues()
        {
            switch (Source)
            {
                case IList sourceList when Target is IList targetList:
                {
                    foreach (var item in sourceList)
                    {
                        if (item == null || targetList.Contains(item))
                        {
                            continue;
                        }
                        targetList.Add(item);
                    }
                    return this;
                }
                case IDictionary sourceDic when Target is IDictionary targetDic:
                {
                    var e = sourceDic.GetEnumerator();
                    e.Reset();
                    while (e.MoveNext())
                    {
                        if (e.Key == null)
                        {
                            continue;
                        }
                        targetDic[e.Key] = e.Value;
                    }
                    return this;
                }
                default:
                    return this;
            }
        }

        internal CollectionBinding Bind()
        {
            if (Mode == BindingMode.OneTime)
            {
                return this;
            }
            if (SourceBindingPath.Source is not IObservableCollection sourceObservable)
            {
                return this;
            }
            sourceObservable.Subscribe(TargetBindingPath);

            if (Mode != BindingMode.TwoWay)
            {
                return this;
            }

            if (TargetBindingPath.Source is not IObservableCollection targetObservable)
            {
                return this;
            }
            targetObservable.Subscribe(SourceBindingPath);
            return this;
        }
    }
}
