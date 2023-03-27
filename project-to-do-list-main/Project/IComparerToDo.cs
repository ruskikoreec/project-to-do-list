using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class SortComparer<ToDoModel> : IComparer<ToDoModel>
    {
        private PropertyDescriptor property;
        private ListSortDirection direction;

        public SortComparer(string propertyName, ListSortDirection direction)
        {
            this.property = TypeDescriptor.GetProperties(typeof(ToDoModel))[propertyName];
            this.direction = direction;
        }

        public int Compare(ToDoModel x, ToDoModel y)
        {
            object xValue = property.GetValue(x);
            object yValue = property.GetValue(y);

            int result = Comparer.Default.Compare(xValue, yValue);

            if (direction == ListSortDirection.Descending)
            {
                result = -result;
            }

            return result;
        }
    }
}
