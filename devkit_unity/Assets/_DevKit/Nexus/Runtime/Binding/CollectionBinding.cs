using System.Collections;
using DevKit.Core.Observables.API;
using DevKit.Nexus.Binding.API;
using DevKit.Nexus.Binding.Internals;

namespace DevKit.Nexus.Binding
{
    public class CollectionBinding : Binding
    {
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

        public override IBinding InitValues()
        {
            switch (SourceBindingPath.Source)
            {
                case IEnumerable sourceList when TargetBindingPath.Source is IList targetList:
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
                case IDictionary sourceDic when TargetBindingPath.Source is IDictionary targetDic:
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
                {
                    return this;
                }
            }
        }

        public override IBinding Bind()
        {
            if (Mode == BindingMode.OneTime)
            {
                return this;
            }
            if (SourceBindingPath.Source is not IObservableCollection sourceObservable)
            {
                return this;
            }
            sourceObservable.Subscribe(TargetBindingPath as CollectionBindingPath);

            if (Mode != BindingMode.TwoWay)
            {
                return this;
            }

            if (TargetBindingPath.Source is not IObservableCollection targetObservable)
            {
                return this;
            }
            targetObservable.Subscribe(SourceBindingPath as CollectionBindingPath);
            return this;
        }
    }
}
