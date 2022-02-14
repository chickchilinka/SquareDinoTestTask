using System.Collections.Generic;
namespace Base
{
    public abstract class FieldBase {
        public delegate void OnValueChange();
        protected List<OnValueChange> monitors;
        public void AddMonitor(OnValueChange monitor) {
            monitors.Add(monitor);
        }

        public void AddMonitorAndCall(OnValueChange monitor) {
            monitors.Add(monitor);
            monitor();
        }
    }
    public class SimpleField<T>:FieldBase
    {
        private string fieldName;
        public string Name {
            get => fieldName;
            set {
                fieldName = value;
            }
        }
        private T fieldValue;
        public T Value {
            get => fieldValue;
            set {
                fieldValue = value;
                foreach (var handler in monitors) {
                    handler();
                }
            }
        }

        public SimpleField() {
            monitors = new List<OnValueChange>();
        }
        public SimpleField(string fieldName):this() {
            this.fieldName = fieldName;
        }
        public SimpleField(string fieldName, T defaultValue) : this(fieldName) {
            fieldValue = defaultValue;
        }
    }
}
