namespace Huffman;

public class BinaryHeap<T>
    {
        private T[] _elements;
        private IComparer<T> _comparer;
        private int _count;

        public BinaryHeap(int capacity, IComparer<T> comparer)
        {
            _elements = new T[capacity];
            _comparer = comparer;
            _count = 0;
        }

        public int Count
        {
            get { return _count; }
        }
        
        public void HeapifyUp(int startIndex)
        {
            int index = startIndex;
            T element = _elements[index];

            while (index > 0)
            {
                int parentIndex = (index - 1) / 2;

                // Check if the parent element is less than or equal to the current element
                if (_comparer.Compare(_elements[parentIndex], element) <= 0)
                {
                    break;
                }

                // Swap the parent element with the current element
                _elements[index] = _elements[parentIndex];
                _elements[parentIndex] = element;

                // Update the index to the parent index
                index = parentIndex;
            }
        }

        public void Insert(T element)
        {
            // Add the element to the end of the array
            _elements[_count] = element;
            _count++;

            // Call HeapifyUp to maintain the min-heap property
            HeapifyUp(_count - 1);
        }


        public T ExtractMin()
        {
            // Remove the minimum element from the heap and rebuild heap
            T result = _elements[0];
            _count--;

            if (_count > 0)
            {
                T lastElement = _elements[_count];
                _elements[0] = lastElement; // Place the last element at the top of the heap

                // Call Heapify to maintain the min-heap property
                Heapify(0);
            }

            return result;
        }

        public void Heapify(int startIndex)
        {
            int index = startIndex;

            while (true)
            {
                int leftChildIndex = (2 * index) + 1;
                int rightChildIndex = (2 * index) + 2;
                if (leftChildIndex >= _count)
                {
                    break;
                }

                int childIndex =
                    (rightChildIndex >= _count ||
                     _comparer.Compare(_elements[leftChildIndex], _elements[rightChildIndex]) <= 0)
                        ? leftChildIndex
                        : rightChildIndex;

                // Check if the parent element is less than or equal to the child element
                if (_comparer.Compare(_elements[index], _elements[childIndex]) <= 0)
                {
                    break;
                }

                // Swap the parent element with the child element
                T temp = _elements[index];
                _elements[index] = _elements[childIndex];
                _elements[childIndex] = temp;

                // Update the index to the child index
                index = childIndex;
            }
        }

    }