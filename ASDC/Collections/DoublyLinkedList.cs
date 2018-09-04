using System;
using System.Collections;
using System.Collections.Generic;

namespace ASDC.Collections
{
	/// <summary>
	/// Представляет строго типизированный двусвязный список объектов.
	/// </summary>
	/// <typeparam name="T">Тип элементов в списке.</typeparam>
	public class DoublyLinkedList<T> : ICollection<T>, ICollection, ICloneable, IReadOnlyCollection<T>
	{

		#region ~Fields~

		// Голова двусвязного списка.
		internal DoublyNode _head;

		// Хвост двусвязного списка.
		internal DoublyNode _tail;

		// Количество элементов в двусвязном списке.
		int count;

		#endregion

		#region ~Contructors~

		/// <summary>
		/// Инициализирует новый экземпляр класса <see cref="DoublyLinkedList{T}"/>, который является пустым.
		/// </summary>
		public DoublyLinkedList()
		{
			SyncRoot = new object();
		}

		/// <summary>
		/// Инициализирует новый экземпляр <see cref="DoublyLinkedList{T}"/>, который содержит элементы, скопированные из указанной коллекции.
		/// </summary>
		/// <param name="collection">Последовательность, элементы которой копируются в новый список.</param>
		/// <exception cref="ArgumentNullException">Свойство <paramref name="collection"/> имеет значение <see cref="null"/>.</exception>
		public DoublyLinkedList(IEnumerable<T> collection) : this()
		{
			foreach (var item in collection)
				PushBack(item);
		}

		#endregion

		#region ~Properties~

		/// <summary>
		/// Получает число элементов, содержащихся в двусвязном списке <see cref="DoublyLinkedList{T}"/>.
		/// </summary>
		public int Count => count;

		/// <summary>
		/// Получает значение, указывающее, является ли объект <see cref="DoublyLinkedList{T}"/> доступным только для чтения.
		/// </summary>
		/// <value>Значение <see cref="true"/>, если объект <see cref="DoublyLinkedList{T}"/> доступен только для чтения; в противном случае — значение <see cref="false"/>.</value>
		public bool IsReadOnly => false;

		/// <summary>
		/// Получает объект, с помощью которого можно синхронизировать доступ к двусвязному списку <see cref="DoublyLinkedList{T}"/>.
		/// </summary>
		/// <value>Объект <see cref="object"/>, который может использоваться для синхронизации доступа к двусвязному списку <see cref="DoublyLinkedList{T}"/>.</value>
		public object SyncRoot { get; }

		/// <summary>
		/// Возвращает значение, показывающее, является ли доступ к двусвязному списку <see cref="DoublyLinkedList{T}"/> синхронизированным (потокобезопасным).
		/// </summary>
		/// <value><see cref="true"/>, если доступ к <see cref="DoublyLinkedList{T}"/> является синхронизированным (потокобезопасным); в противном случае — <see cref="false"/>. Значение по умолчанию — <see cref="false"/>.</value>
		public bool IsSynchronized { get; }

		#endregion

		#region ~Methods~

		/// <summary>
		/// Добавляет объект в конец двусвязного списка <see cref="DoublyLinkedList{T}"/>.
		/// </summary>
		/// <param name="item">Объект, добавляемый в конец двусвязного списка <see cref="DoublyLinkedList{T}"/>. Для ссылочных типов допускается значение <see cref="null"/>.</param>
		void ICollection<T>.Add(T item) => PushBack(item);

		/// <summary>
		/// Добавляет объект в конец двусвязного списка <see cref="DoublyLinkedList{T}"/>.
		/// </summary>
		/// <param name="item">Объект, добавляемый в конец двусвязного списка <see cref="DoublyLinkedList{T}"/>. Для ссылочных типов допускается значение <see cref="null"/>.</param>
		public void PushBack(T item)
		{
			DoublyNode itemNode = new DoublyNode(item)
			{
				Back = _tail
			};
			if (_head == null)
				_head = itemNode;
			else
				_tail.Next = itemNode;
			_tail = itemNode;
			count++;
		}

		/// <summary>
		/// Добавляет объект в начало двусвязного списка <see cref="DoublyLinkedList{T}"/>.
		/// </summary>
		/// <param name="item">Объект, добавляемый в начало двусвязного списка <see cref="DoublyLinkedList{T}"/>. Для ссылочных типов допускается значение <see cref="null"/>.</param>
		public void PushFront(T item)
		{
			DoublyNode itemNode = new DoublyNode(item)
			{
				Next = _head
			};
			if (_tail == null)
				_tail = itemNode;
			else
				_head.Back = itemNode;
			_head = itemNode;
			count++;
		}

		/// <summary>
		/// Удаляет все элементы из двусвязного списка <see cref="DoublyLinkedList{T}"/>.
		/// </summary>
		public void Clear()
		{
			// Молитва сборщику мусора
			_head = _tail = null;
			count = 0;
		}

		/// <summary>
		/// Создает неполную копию <see cref="DoublyLinkedList{T}"/>.
		/// </summary>
		/// <returns>Неполная копия <see cref="DoublyLinkedList{T}"/>.</returns>
		public object Clone()
		{
			return new DoublyLinkedList<T>(this);
		}

		/// <summary>
		/// Определяет, входит ли элемент в двусвязный список <see cref="DoublyLinkedList{T}"/>.
		/// </summary>
		/// <param name="item">Объект для поиска в <see cref="DoublyLinkedList{T}"/>. Для ссылочных типов допускается значение <see cref="null"/>.</param>
		/// <returns>Значение <see cref="true"/>, если параметр <paramref name="item"/> найден в двусвязном списке <see cref="DoublyLinkedList{T}"/>; в противном случае — значение <see cref="false"/>.</returns>
		public bool Contains(T item)
		{
			DoublyNode current = _head;
			while (current != null && !current.Value.Equals(item))
				current = current.Next;
			return current != null;
		}

		/// <summary>
		/// Копирует <see cref="DoublyLinkedList{T}"/> целиком в совместимый одномерный массив, начиная с указанного индекса конечного массива.
		/// </summary>
		/// <param name="array">Одномерный массив <see cref="Array"/>, в который копируются элементы из двусвязного списка <see cref="DoublyLinkedList{T}"/>. Массив <see cref="Array"/> должен иметь индексацию, начинающуюся с нуля.</param>
		/// <param name="arrayIndex">Отсчитываемый от нуля индекс в массиве array, указывающий начало копирования.</param>
		/// <exception cref="ArgumentNullException">Свойство <paramref name="array"/> имеет значение <see cref="null"/>.</exception>
		/// <exception cref="ArgumentOutOfRangeException">Значение параметра <paramref name="arrayIndex"/> меньше 0.</exception>
		/// <exception cref="ArgumentException">Число элементов в исходном двусвязном списке <see cref="DoublyLinkedList{T}"/> больше доступного места от положения, заданного значением параметра <paramref name="arrayIndex"/>, до конца массива назначения <paramref name="array"/>.</exception>
		public void CopyTo(T[] array, int arrayIndex)
		{
			if (array == null)
				throw new ArgumentNullException(nameof(array));
			if (arrayIndex < 0)
				throw new ArgumentOutOfRangeException(nameof(arrayIndex));
			if (array.Length - arrayIndex < count)
				throw new ArgumentException("Длина результирующего массива недостаточна.", nameof(arrayIndex));
			using (var enumerator = GetDoublyEnumerator())
				for (int i = arrayIndex; enumerator.MoveNext(); i++)
					array[i] = enumerator.Current;
		}

		/// <summary>
		/// Копирует <see cref="DoublyLinkedList{T}"/> целиком в совместимый одномерный массив, начиная с указанного индекса конечного массива.
		/// </summary>
		/// <param name="array">Одномерный массив <see cref="Array"/>, в который копируются элементы из двусвязного списка <see cref="DoublyLinkedList{T}"/>. Массив <see cref="Array"/> должен иметь индексацию, начинающуюся с нуля.</param>
		/// <param name="arrayIndex">Отсчитываемый от нуля индекс в массиве array, указывающий начало копирования.</param>
		/// <exception cref="ArgumentNullException">Свойство <paramref name="array"/> имеет значение <see cref="null"/>.</exception>
		/// <exception cref="ArgumentOutOfRangeException">Значение параметра <paramref name="index"/> меньше 0.</exception>
		/// <exception cref="ArgumentException">Число элементов в исходном двусвязном списке <see cref="DoublyLinkedList{T}"/> больше доступного места от положения, заданного значением параметра <paramref name="index"/>, до конца массива назначения <paramref name="array"/>.</exception>
		public void CopyTo(Array array, int index)
		{
			if (array == null)
				throw new ArgumentNullException(nameof(array));
			if (index < 0)
				throw new ArgumentOutOfRangeException(nameof(index));
			if (array.Length - index < count)
				throw new ArgumentException("Длина результирующего массива недостаточна.", nameof(index));
			using (var enumerator = GetEnumerator())
				for (int i = index; enumerator.MoveNext(); i++)
					array.SetValue(enumerator.Current, i);
		}

		/// <summary>
		/// Вставляет элемент в двусвязный список <see cref="DoublyLinkedList{T}"/> после элемента, удовлетворяющего условиям указанного предиката.
		/// </summary>
		/// <param name="predicate">Делегат <see cref="Predicate{T}"/>, определяющий условия поиска элемента.</param>
		/// <param name="item">Вставляемый объект. Для ссылочных типов допускается значение <see cref="null"/>.</param>
		/// <returns>Значение <see cref="true"/>, если элемент <paramref name="item"/> успешно вставлен, в противном случае — значение <see cref="false"/>. Этот метод также возвращает <see cref="false"/>, если элемент, удовлетворяющий условиям указанного предиката <paramref name="predicate"/>, не найден в двусвязном списке <see cref="DoublyLinkedList{T}"/>.</returns>
		/// <exception cref="ArgumentNullException">Свойство <paramref name="predicate"/> имеет значение <see cref="null"/>.</exception>
		public bool InsertAfter(Predicate<T> predicate, T item)
		{
			if (predicate == null)
				throw new ArgumentNullException(nameof(predicate));
			DoublyNode current = _head;
			while (current != null && !predicate(current.Value))
				current = current.Next;
			if (current == null)
				return false;
			var next = current.Next;
			next.Back = current.Next = new DoublyNode(item)
			{
				Back = current,
				Next = current.Next
			};
			if (current == _tail)
				_tail = current.Next;
			count++;
			return true;
		}

		/// <summary>
		/// Вставляет элемент в двусвязный список <see cref="DoublyLinkedList{T}"/> перед элементом, удовлетворяющим условиям указанного предиката.
		/// </summary>
		/// <param name="predicate">Делегат <see cref="Predicate{T}"/>, определяющий условия поиска элемента.</param>
		/// <param name="item">Вставляемый объект. Для ссылочных типов допускается значение <see cref="null"/>.</param>
		/// <returns>Значение <see cref="true"/>, если элемент <paramref name="item"/> успешно вставлен, в противном случае — значение <see cref="false"/>. Этот метод также возвращает <see cref="false"/>, если элемент, удовлетворяющий условиям указанного предиката <paramref name="predicate"/>, не найден в двусвязном списке <see cref="DoublyLinkedList{T}"/>.</returns>
		/// <exception cref="ArgumentNullException">Свойство <paramref name="predicate"/> имеет значение <see cref="null"/>.</exception>
		public bool InsertBefore(Predicate<T> predicate, T item)
		{
			if (predicate == null)
				throw new ArgumentNullException(nameof(predicate));
			DoublyNode current = _tail;
			while (current != null && !predicate(current.Value))
				current = current.Back;
			if (current == null)
				return false;
			var back = current.Back;
			back.Next = current.Back = new DoublyNode(item)
			{
				Next = current,
				Back = current.Back
			};
			if (current == _head)
				_head = current.Back;
			count++;
			return true;
		}

		/// <summary>
		/// Удаляет первое вхождение указанного объекта из двусвязного списка <see cref="DoublyLinkedList{T}"/>.
		/// </summary>
		/// <param name="item">Объект, который необходимо удалить из двусвязного списка <see cref="DoublyLinkedList{T}"/>. Для ссылочных типов допускается значение <see cref="null"/>.</param>
		/// <returns>Значение <see cref="true"/>, если элемент <paramref name="item"/> успешно удален, в противном случае — значение <see cref="false"/>. Этот метод также возвращает <see cref="false"/>, если элемент <paramref name="item"/> не найден в двусвязном списке <see cref="DoublyLinkedList{T}"/>.</returns>
		public bool Remove(T item)
		{
			var result = false;
			if (_head == null || _tail == null)
				return result;
			DoublyNode current = _head;
			if (item.Equals(_head.Value))
			{
				_head = _head.Next;
				if (_head != null)
					_head.Back = null;
				else
					_tail = null;
				result = true;
				count--;
			}
			else if(item.Equals(_tail.Value))
			{
				_tail = _tail.Back;
				if (_tail != null)
					_tail.Next = null;
				else
					_head = null;
				result = true;
				count--;
			}
			else
			{
				while (current.Next != null && !current.Next.Value.Equals(item))
					current = current.Next;
				(current.Next = current.Next.Next).Back = current;
				result = true;
				count--;
			}
			return result;
		}

		/// <summary>
		/// Возвращает перечислитель, осуществляющий перебор элементов двусвязного списка <see cref="DoublyLinkedList{T}"/>.
		/// </summary>
		/// <returns>Новый объект <see cref="IEnumerator{T}"/> для <see cref="DoublyLinkedList{T}"/>.</returns>
		public IEnumerator<T> GetEnumerator() => new DoublyEnumerator(this);

		/// <summary>
		/// Возвращает перечислитель, осуществляющий перебор элементов двусвязного списка <see cref="DoublyLinkedList{T}"/>.
		/// </summary>
		/// <returns>Новый объект <see cref="IEnumerator"/> для <see cref="DoublyLinkedList{T}"/>.</returns>
		IEnumerator IEnumerable.GetEnumerator() => new DoublyEnumerator(this);

		/// <summary>
		/// Возвращает перечислитель, осуществляющий перебор элементов двусвязного списка <see cref="DoublyLinkedList{T}"/>.
		/// </summary>
		/// <returns>Новый объект <see cref="DoublyEnumerator"/> для <see cref="DoublyLinkedList{T}"/>.</returns>
		public DoublyEnumerator GetDoublyEnumerator() => new DoublyEnumerator(this);

		#endregion

		#region ~Classes~

		/// <summary>
		/// Представляет узел строго типизированного двусвязного списка объектов <see cref="DoublyLinkedList{T}"/>.
		/// </summary>
		internal class DoublyNode
		{

			#region ~Properties~

			/// <summary>
			/// Получает следующий узел <see cref="DoublyNode"/> двусвязного списка <see cref="DoublyLinkedList{T}"/>.
			/// </summary>
			public DoublyNode Next { get; set; }

			/// <summary>
			/// Получает предыдущий узел <see cref="DoublyNode"/> двусвязного списка <see cref="DoublyLinkedList{T}"/>.
			/// </summary>
			public DoublyNode Back { get; set; }

			/// <summary>
			/// Получает значение текущего узла <see cref="DoublyNode"/> двусвязного списка <see cref="DoublyLinkedList{T}"/>.
			/// </summary>
			public T Value { get; }

			#endregion

			#region ~Constructors~

			/// <summary>
			/// Инициализирует новый экземпляр класса <see cref="DoublyNode"/> с указанным в <paramref name="value"/> значением.
			/// </summary>
			/// <param name="value">Значение, которое хранится в текущем узле.</param>
			public DoublyNode(T value) => Value = value;

			#endregion

		}

		/// <summary>
		/// Выполняет перечисление элементов двусвязного списка <see cref="DoublyLinkedList{T}"/>.
		/// </summary>
		public struct DoublyEnumerator : IEnumerator<T>, IDisposable
		{

			#region ~Fields~

			// Односвязный список.
			internal DoublyLinkedList<T> _list;

			// Текущий узел двусвязного списка.
			internal DoublyNode _currentNode;

			// Следующий узел двусвязного списка.
			internal DoublyNode _nextNode;

			// Предыдущий узел двусвязного списка.
			internal DoublyNode _backNode;

			#endregion

			#region ~Properties~

			/// <summary>
			/// Возвращает элемент, расположенный в текущей позиции перечислителя.
			/// </summary>
			/// <value>Элемент двусвязного списка <see cref="DoublyLinkedList{T}"/>, находящийся в текущей позиции перечислителя.</value>
			/// <exception cref="InvalidOperationException">Перечеслитель находится в начальном положении, т. е. перед первым или после последнего элемента двусвязного списка.</exception>
			public T Current
			{
				get
				{
					if (_currentNode == null)
						throw new InvalidOperationException();
					return _currentNode.Value;
				}
			}

			object IEnumerator.Current => Current;

			#endregion

			#region ~Constructors~

			internal DoublyEnumerator(DoublyLinkedList<T> list)
			{
				_list = list;
				_currentNode = null;
				_nextNode = _list._head;
				_backNode = _list._tail;
			}

			#endregion

			#region ~Methods~

			/// <summary>
			/// Перемещает перечислитель к следующему элементу двусвязного списка <see cref="DoublyLinkedList{T}"/>.
			/// </summary>
			/// <returns>Значение <see cref="true"/>, если перечислитель был успешно перемещен к следующему элементу; значение <see cref="false"/>, если перечислитель достиг конца двусвязного списка.</returns>
			public bool MoveNext()
			{
				_backNode = _currentNode;
				_currentNode = _nextNode;
				if (_currentNode == null)
					return false;
				_nextNode = _nextNode.Next;
				return true;
			}

			/// <summary>
			/// Перемещает перечислитель к предыдущему элементу двусвязного списка <see cref="DoublyLinkedList{T}"/>.
			/// </summary>
			/// <returns>Значение <see cref="true"/>, если перечислитель был успешно перемещен к предыдущему элементу; значение <see cref="false"/>, если перечислитель достиг начала двусвязного списка.</returns>
			public bool MoveBack()
			{
				_nextNode = _currentNode;
				_currentNode = _backNode;
				if (_currentNode == null)
					return false;
				_backNode = _backNode.Back;
				return true;
			}

			/// <summary>
			/// Устанавливает перечислитель в его начальное положение, т. е. перед первым элементом двусвязного списка.
			/// </summary>
			void IEnumerator.Reset() => ResetFront();

			/// <summary>
			/// Устанавливает перечислитель в его начальное положение, т. е. перед первым элементом двусвязного списка.
			/// </summary>
			public void ResetFront()
			{
				_backNode = _currentNode = null;
				_nextNode = _list._head;
			}

			/// <summary>
			/// Устанавливает перечислитель в его конечное положение, т. е. после последнего элемента двусвязного списка.
			/// </summary>
			public void ResetBack()
			{
				_currentNode = _nextNode = null;
				_nextNode = _list._head;
			}

			/// <summary>
			/// Освобождает все ресурсы, занятые перечеслителем <see cref="DoublyEnumerator"/>.
			/// </summary>
			public void Dispose()
			{
				_list = null;
				_backNode = _currentNode = _nextNode = null;
			}

			#endregion

		}

		#endregion

	}
}
