using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Tribot
{
    /// <summary>
    /// Generic singleton container that is managed by the container manager 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Container<T> : Singleton<Container<T>>, IContainer
    {
        private List<T> _items;

        public List<T> Items
        {
            get
            {
                if (_items == null)
                {
                    _items = new List<T>();
                }
                return _items;
            }
            set { _items = value; }
        }

        public Container()
        {
            ContainerManager.Instance.Add(this);
        }

        public void Add(T item)
        {
            if (_items == null)
                _items = new List<T>();

            if (!_items.Contains(item))
                _items.Add(item);
        }

        public void Clear()
        {
            if (_items != null)
                _items.Clear();
        }

        public void Remove(T item)
        {
            if (_items != null)
            {
                _items.Remove(item);
            }
        }
    }

    public class ContainerManager : Singleton<ContainerManager>
    {
        public List<IContainer> Containers = new List<IContainer>();

        public void Add(IContainer container)
        {
            Containers.Add(container);
        }

        private void CleanUpInstance()
        {
            foreach (var container in Containers)
            {
                container.Clear();
            }
        }

        public static void CleanUp()
        {
            Instance.CleanUpInstance();
        }
    }

    public interface IContainer
    {
        void Clear();
    }
}


