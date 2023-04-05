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

        public void Insert(T element)
        {
            // Add the element to the end of the array
            _elements[_count] = element;
            _count++;

            // Change elements
            int index = _count - 1;
            while (index > 0)
            {
                int parentIndex = (index - 1) / 2;
                T parent = _elements[parentIndex];
                if (_comparer.Compare(parent, element) <= 0)
                {
                    break;
                }

                _elements[index] = parent;
                index = parentIndex;
            }

            _elements[index] = element;
        }


        public T ExtractMin()
        {
            // Remove the minimum element from the heap and rebuild heap
            T result = _elements[0];
            _count--;
            if (_count > 0)
            {
                T lastElement = _elements[_count];
                int index = 0;
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
                    T child = _elements[childIndex];
                    if (_comparer.Compare(lastElement, child) <= 0)
                    {
                        break;
                    }

                    _elements[index] = child;
                    index = childIndex;
                }

                _elements[index] = lastElement;
            }

            return result;
        }
    }