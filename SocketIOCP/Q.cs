using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketIOCP
{
    public class Q<T>
    {
        private Node<T> head;
        private Node<T> tail;

        private int count;

        public void Add(T element)
        {
            Node<T> node = new Node<T>(element);

            if (this.head == null)
            {
                this.head = node;
                this.tail = node;

                this.count = 1;

                return;
            }
             
   
            this.tail.Next = node;
            this.tail = node;

            this.count++;
        }

        public T Get()
        {

            if (this.head == null)
                return default(T);


            //Node<T> node;


            //if (this.head.Next == null)
            //{
            //    node = this.head;

            //    this.head = null;

            //    this.count--;

            //    return node;
            //}


            Node<T> node = this.head;
            this.head = node.Next;

            this.count--;

            return node.element;

        }

        public int Count
        {
            get { return this.count; }
        }
        //public void Remove(Node<T> node)
        //{
        //    node.Before.Next = node.Next;
        //}

        public class Node<T>
        {
            //public Node<T> Before;
            public Node<T> Next;

            public T element;

            public Node(T element)
            {
                this.element = element;
            }
        }
    }
}
