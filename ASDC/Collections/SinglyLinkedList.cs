using System;
using System.Collections;
using System.Collections.Generic;

namespace ASDC.Collections
{
	/// <summary>
	/// Представляет строго типизированный односвязный список объектов.
	/// </summary>
	/// <typeparam name="T">Тип элементов в списке.</typeparam>
	public class SinglyLinkedList<T> : ICollection<T>, ICollection, ICloneable, IReadOnlyCollection<T>
	{
		
		#region ~Fields~

		// Голова односвязного списка.
		private SinglyNode _head;

		// Количество элементов в односвязном списке.
		int count;

		#endregion

		#region ~Constructors~

		/// <summary>
		/// Инициализирует новый экземпляр класса <see cref="SinglyLinkedList{T}"/>, который является пустым.
		/// </summary>
		public SinglyLinkedList()
		{
			SyncRoot = new object();
		}

		/// <summary>
		/// Инициализирует новый экземпляр <see cref="SinglyLinkedList{T}"/>, который содержит элементы, скопированные из указанной коллекции.
		/// </summary>
		/// <param name="collection">Последовательность, элементы которой копируются в новый список.</param>
		/// <exception cref="ArgumentNullException">Свойство <paramref name="collection"/> имеет значение <see cref="null"/>.</exception>
		public SinglyLinkedList(IEnumerable<T> collection) : this()
		{
			if (collection == null)
				throw new ArgumentNullException(nameof(collection));
			foreach (var item in collection)
				Add(item);
		}

		#endregion

		#region ~Properties~

		/// <summary>
		/// Получает число элементов, содержащихся в односвязном списке <see cref="SinglyLinkedList{T}"/>.
		/// </summary>
		public int Count => count;

		/// <summary>
		/// Получает значение, указывающее, является ли объект <see cref="SinglyLinkedList{T}"/> доступным только для чтения.
		/// </summary>
		/// <value>Значение <see cref="true"/>, если объект <see cref="SinglyLinkedList{T}"/> доступен только для чтения; в противном случае — значение <see cref="false"/>.</value>
		public bool IsReadOnly => false;

		/// <summary>
		/// Получает объект, с помощью которого можно синхронизировать доступ к односвязному списку <see cref="SinglyLinkedList{T}"/>.
		/// </summary>
		/// <value>Объект <see cref="object"/>, который может использоваться для синхронизации доступа к односвязному списку <see cref="SinglyLinkedList{T}"/>.</value>
		public object SyncRoot { get; }

		/// <summary>
		/// Возвращает значение, показывающее, является ли доступ к односвязному списку <see cref="SinglyLinkedList{T}"/> синхронизированным (потокобезопасным).
		/// </summary>
		/// <value><see cref="true"/>, если доступ к <see cref="SinglyLinkedList{T}"/> является синхронизированным (потокобезопасным); в противном случае — <see cref="false"/>. Значение по умолчанию — <see cref="false"/>.</value>
		public bool IsSynchronized => false;

		#endregion

		#region ~Methods~

		/// <summary>
		/// Добавляет объект в конец односвязного списка <see cref="SinglyLinkedList{T}"/>.
		/// </summary>
		/// <param name="item">Объект, добавляемый в конец односвязного списка <see cref="SinglyLinkedList{T}"/>. Для ссылочных типов допускается значение <see cref="null"/>.</param>
		public void Add(T item)
		{
			SinglyNode itemNode = new SinglyNode(item);
			if (_head == null)
				_head = itemNode;
			else
			{
				SinglyNode current = _head;
				while (current.Next != null)
					current = current.Next;
				current.Next = itemNode;
			}
			count++;
		}

		/// <summary>
		/// Удаляет все элементы из односвязного списка <see cref="SinglyLinkedList{T}"/>.
		/// </summary>
		public void Clear()
		{
			// Молитва сборщику мусора
			_head = null;
			count = 0;
		}

		/// <summary>
		/// Создает неполную копию <see cref="SinglyLinkedList{T}"/>.
		/// </summary>
		/// <returns>Неполная копия <see cref="SinglyLinkedList{T}"/>.</returns>
		public object Clone()
		{
			return new SinglyLinkedList<T>(this);
		}

		/// <summary>
		/// Определяет, входит ли элемент в односвязный список <see cref="SinglyLinkedList{T}"/>.
		/// </summary>
		/// <param name="item">Объект для поиска в <see cref="SinglyLinkedList{T}"/>. Для ссылочных типов допускается значение <see cref="null"/>.</param>
		/// <returns>Значение <see cref="true"/>, если параметр <paramref name="item"/> найден в односвязном списке <see cref="SinglyLinkedList{T}"/>; в противном случае — значение <see cref="false"/>.</returns>
		public bool Contains(T item)
		{
			SinglyNode current = _head;
			while (current != null && !current.Value.Equals(item))
				current = current.Next;
			return current != null;
		}

		/// <summary>
		/// Копирует <see cref="SinglyLinkedList{T}"/> целиком в совместимый одномерный массив, начиная с указанного индекса конечного массива.
		/// </summary>
		/// <param name="array">Одномерный массив <see cref="Array"/>, в который копируются элементы из односвязного списка <see cref="SinglyLinkedList{T}"/>. Массив <see cref="Array"/> должен иметь индексацию, начинающуюся с нуля.</param>
		/// <param name="arrayIndex">Отсчитываемый от нуля индекс в массиве array, указывающий начало копирования.</param>
		/// <exception cref="ArgumentNullException">Свойство <paramref name="array"/> имеет значение <see cref="null"/>.</exception>
		/// <exception cref="ArgumentOutOfRangeException">Значение параметра <paramref name="arrayIndex"/> меньше 0.</exception>
		/// <exception cref="ArgumentException">Число элементов в исходном односвязном списке <see cref="SinglyLinkedList{T}"/> больше доступного места от положения, заданного значением параметра <paramref name="arrayIndex"/>, до конца массива назначения <paramref name="array"/>.</exception>
		public void CopyTo(T[] array, int arrayIndex)
		{
			if (array == null)
				throw new ArgumentNullException(nameof(array));
			if (arrayIndex < 0)
				throw new ArgumentOutOfRangeException(nameof(arrayIndex));
			if (array.Length - arrayIndex < count)
				throw new ArgumentException("Длина результирующего массива недостаточна.", nameof(arrayIndex));
			using (var enumerator = GetSinglyEnumerator())
				for (int i = arrayIndex; enumerator.MoveNext(); i++)
					array[i] = enumerator.Current;
		}

		/// <summary>
		/// Копирует <see cref="SinglyLinkedList{T}"/> целиком в совместимый одномерный массив, начиная с указанного индекса конечного массива.
		/// </summary>
		/// <param name="array">Одномерный массив <see cref="Array"/>, в который копируются элементы из односвязного списка <see cref="SinglyLinkedList{T}"/>. Массив <see cref="Array"/> должен иметь индексацию, начинающуюся с нуля.</param>
		/// <param name="index">Отсчитываемый от нуля индекс в массиве array, указывающий начало копирования.</param>
		/// <exception cref="ArgumentNullException">Свойство <paramref name="array"/> имеет значение <see cref="null"/>.</exception>
		/// <exception cref="ArgumentOutOfRangeException">Значение параметра <paramref name="index"/> меньше 0.</exception>
		/// <exception cref="ArgumentException">Число элементов в исходном односвязном списке <see cref="SinglyLinkedList{T}"/> больше доступного места от положения, заданного значением параметра <paramref name="index"/>, до конца массива назначения <paramref name="array"/>.</exception>
		public void CopyTo(Array array, int index)
		{
			if (array == null)
				throw new ArgumentNullException(nameof(array));
			if (index < 0)
				throw new ArgumentOutOfRangeException(nameof(index));
			if (array.Length - index < count)
				throw new ArgumentException("Длина результирующего массива недостаточна.", nameof(index));
			using (var enumerator = GetSinglyEnumerator())
				for (int i = index; enumerator.MoveNext(); i++)
					array.SetValue(enumerator.Current, i);
		}

		/// <summary>
		/// Вставляет элемент в односвязный список <see cref="SinglyLinkedList{T}"/> после элемента, удовлетворяющего условиям указанного предиката.
		/// </summary>
		/// <param name="predicate">Делегат <see cref="Predicate{T}"/>, определяющий условия поиска элемента.</param>
		/// <param name="item">Вставляемый объект. Для ссылочных типов допускается значение <see cref="null"/>.</param>
		/// <returns>Значение <see cref="true"/>, если элемент <paramref name="item"/> успешно вставлен, в противном случае — значение <see cref="false"/>. Этот метод также возвращает <see cref="false"/>, если элемент, удовлетворяющий условиям указанного предиката <paramref name="predicate"/>, не найден в односвязном списке <see cref="SinglyLinkedList{T}"/>.</returns>
		/// <exception cref="ArgumentNullException">Свойство <paramref name="predicate"/> имеет значение <see cref="null"/>.</exception>
		public bool Insert(Predicate<T> predicate, T item)
		{
			if (predicate == null)
				throw new ArgumentNullException(nameof(predicate));
			SinglyNode current = _head;
			while (current != null && !predicate(current.Value))
				current = current.Next;
			if (current == null)
				return false;
			current.Next = new SinglyNode(item)
			{
				Next = current.Next
			};
			count++;
			return true;
		}
		
		/// <summary>
		/// Удаляет первое вхождение указанного объекта из односвязного списка <see cref="SinglyLinkedList{T}"/>.
		/// </summary>
		/// <param name="item">Объект, который необходимо удалить из односвязного списка <see cref="SinglyLinkedList{T}"/>. Для ссылочных типов допускается значение <see cref="null"/>.</param>
		/// <returns>Значение <see cref="true"/>, если элемент <paramref name="item"/> успешно удален, в противном случае — значение <see cref="false"/>. Этот метод также возвращает <see cref="false"/>, если элемент <paramref name="item"/> не найден в односвязном списке <see cref="SinglyLinkedList{T}"/>.</returns>
		public bool Remove(T item)
		{
			var result = false;
			if (_head == null)
				return result;
			SinglyNode current = _head;
			if (current.Value.Equals(item))
			{
				_head = current.Next;
				result = true;
				count--;
			}
			else
			{
				while (current.Next != null && !current.Next.Value.Equals(item))
					current = current.Next;
				current.Next = current.Next.Next;
				result = true;
				count--;
			}
			return result;
		}

		/// <summary>
		/// Возвращает перечислитель, осуществляющий перебор элементов односвязного списка <see cref="SinglyLinkedList{T}"/>.
		/// </summary>
		/// <returns>Новый объект <see cref="IEnumerator{T}"/> для <see cref="SinglyLinkedList{T}"/>.</returns>
		public IEnumerator<T> GetEnumerator() => new SinglyEnumerator(this);

		/// <summary>
		/// Возвращает перечислитель, осуществляющий перебор элементов односвязного списка <see cref="SinglyLinkedList{T}"/>.
		/// </summary>
		/// <returns>Новый объект <see cref="IEnumerator"/> для <see cref="SinglyLinkedList{T}"/>.</returns>
		IEnumerator IEnumerable.GetEnumerator() => new SinglyEnumerator(this);

		/// <summary>
		/// Возвращает перечислитель, осуществляющий перебор элементов односвязного списка <see cref="SinglyLinkedList{T}"/>.
		/// </summary>
		/// <returns>Новый объект <see cref="SinglyEnumerator"/> для <see cref="SinglyLinkedList{T}"/>.</returns>
		public SinglyEnumerator GetSinglyEnumerator() => new SinglyEnumerator(this);

		#endregion

		#region ~Classes~

		/// <summary>
		/// Представляет узел строго типизированного односвязного списка объектов <see cref="SinglyLinkedList{T}"/>.
		/// </summary>
		internal class SinglyNode
		{

			#region ~Properties~

			/// <summary>
			/// Получает следующий узел <see cref="SinglyNode"/> односвязного списка <see cref="SinglyLinkedList{T}"/>.
			/// </summary>
			public SinglyNode Next { get; set; }

			/// <summary>
			/// Получает значение текущего узла <see cref="SinglyNode"/> односвязного списка <see cref="SinglyLinkedList{T}"/>.
			/// </summary>
			public T Value { get; }

			#endregion

			#region ~Constructors~

			/// <summary>
			/// Инициализирует новый экземпляр класса <see cref="SinglyNode"/> с указанным в <paramref name="value"/> значением.
			/// </summary>
			/// <param name="value">Значение, которое хранится в текущем узле.</param>
			public SinglyNode(T value) => Value = value;

			#endregion

		}

		/// <summary>
		/// Выполняет перечисление элементов односвязного списка <see cref="SinglyLinkedList{T}"/>.
		/// </summary>
		public struct SinglyEnumerator : IEnumerator<T>, IDisposable
		{

			#region ~Fields~

			// Односвязный список.
			SinglyLinkedList<T> _list;

			// Текущий узел односвязного списка.
			internal SinglyNode _currentNode;

			// Следующий узел односвязного списка.
			internal SinglyNode _nextNode;

			#endregion

			#region ~Properties~

			/// <summary>
			/// Возвращает элемент, расположенный в текущей позиции перечислителя.
			/// </summary>
			/// <value>Элемент односвязного списка <see cref="SinglyLinkedList{T}"/>, находящийся в текущей позиции перечислителя.</value>
			/// <exception cref="InvalidOperationException">Перечеслитель находится в начальном положении, т. е. перед первым элементом коллекции.</exception>
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

			internal SinglyEnumerator(SinglyLinkedList<T> list)
			{
				_list = list;
				_currentNode = null;
				_nextNode = _list._head;
			}

			#endregion

			#region ~Methods~

			/// <summary>
			/// Перемещает перечислитель к следующему элементу односвязного списка <see cref="SinglyLinkedList{T}"/>.
			/// </summary>
			/// <returns>Значение <see cref="true"/>, если перечислитель был успешно перемещен к следующему элементу; значение <see cref="false"/>, если перечислитель достиг конца односвязного списка.</returns>
			public bool MoveNext()
			{
				_currentNode = _nextNode;
				if (_currentNode == null)
					return false;
				_nextNode = _nextNode.Next;
				return true;
			}

			/// <summary>
			/// Устанавливает перечислитель в его начальное положение, т. е. перед первым элементом односвязного списка.
			/// </summary>
			public void Reset()
			{
				_currentNode = null;
				_nextNode = _list._head;
			}

			/// <summary>
			/// Освобождает все ресурсы, занятые модулем <see cref="SinglyEnumerator"/>.
			/// </summary>
			public void Dispose()
			{
				_list = null;
				_currentNode = _nextNode = null;
			}

			#endregion

		}

		#endregion

	}
}
