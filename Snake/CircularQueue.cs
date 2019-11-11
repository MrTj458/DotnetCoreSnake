using System;

namespace Snake
{
    public class CircularQueue
    {
        public Position[] Items { get; }
        private int head = -1;
        private int tail = -1;
        public int size = 0;

        public CircularQueue(int capacity)
        {
            Items = new Position[capacity];
        }

        public void Enqueue(Position item)
        {
            if (this.IsFull())
                throw new Exception();

            if (this.IsEmpty())
                head = 0;
            tail = (tail + 1) % Capacity();
            Items[tail] = item;
            size++;
        }

        public Position Dequeue()
        {
            if (this.IsEmpty())
                throw new Exception();

            Position item = Items[head]; // element to return
            Items[head] = null; // avoid loitering
            if (size > 1)
                head = (head + 1) % Capacity(); // update head
            else
            { // after removing the last element, set head and tail to -1
                head = -1;
                tail = -1;
            }

            size--;
            return item;
        }

        public int Capacity()
        {
            return Items.Length;
        }

        public bool IsEmpty()
        {
            return size == 0;
        }

        public bool IsFull()
        {
            return size == Capacity();
        }
    }
}
