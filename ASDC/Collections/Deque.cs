using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ASDC.Collections
{
	/// <summary>
	/// Представляет двустороннюю очередь объектов, основанную на принципе "первым поступил — первым обслужен (FIFO)".
	/// </summary>
	/// <typeparam name="T">Тип элементов в двусторонней очереди.</typeparam>
	public class Deque<T> : ICollection<T>, ICollection, ICloneable, IReadOnlyCollection<T>
	{

		#region ~Fields~

		// Внутренний список, на базе которого строится двусторонняя очередь
		DoublyLinkedList<T> _list;

		#endregion

		#region ~Properties~

		/// <summary>
		/// Получает число элементов, содержащихся в двусторонней очереди <see cref="Deque{T}"/>.
		/// </summary>
		public int Count => _list.Count;

		/// <summary>
		/// Получает значение, указывающее, является ли объект <see cref="Deque{T}"/> доступным только для чтения.
		/// </summary>
		/// <value>Значение <see cref="true"/>, если объект <see cref="Deque{T}"/> доступен только для чтения; в противном случае — значение <see cref="false"/>.</value>
		public bool IsReadOnly => false;

		/// <summary>
		/// Получает объект, с помощью которого можно синхронизировать доступ к двусторонней очереди <see cref="Deque{T}"/>.
		/// </summary>
		/// <value>Объект <see cref="object"/>, который может использоваться для синхронизации доступа к двусторонней очереди <see cref="Deque{T}"/>.</value>
		public object SyncRoot { get; }

		/// <summary>
		/// Возвращает значение, показывающее, является ли доступ к двусторонней очереди <see cref="Deque{T}"/> синхронизированным (потокобезопасным).
		/// </summary>
		/// <value><see cref="true"/>, если доступ к двусторонней очереди <see cref="Deque{T}"/> является синхронизированным (потокобезопасным); в противном случае — <see cref="false"/>. Значение по умолчанию — <see cref="false"/>.</value>
		public bool IsSynchronized => false;

		#endregion

		#region ~Constructors~

		/// <summary>
		/// Инициализирует новый экземпляр класса <see cref="Deque{T}"/>, который является пустым.
		/// </summary>
		public Deque()
		{
			SyncRoot = new object();
			_list = new DoublyLinkedList<T>();
		}

		/// <summary>
		/// Инициализирует новый экземпляр <see cref="Deque{T}"/>, который содержит элементы, скопированные из указанной коллекции.
		/// </summary>
		/// <param name="collection">Коллекция, элементы которой копируются в двустороннюю очередь.</param>
		/// <exception cref="ArgumentNullException">Свойство <paramref name="collection"/> имеет значение <see cref="null"/>.</exception>
		public Deque(IEnumerable<T> collection)
		{
			if (collection == null)
				throw new ArgumentNullException(nameof(collection));
			SyncRoot = new object();
			_list = new DoublyLinkedList<T>(collection);
		}

		#endregion

		#region ~Methods~

		/// <summary>
		/// Добавляет объект в конец очереди <see cref="Deque{T}"/>.
		/// </summary>
		/// <param name="item">Объект, добавляемый в конец очереди <see cref="Deque{T}"/>. Для ссылочных типов допускается значение <see cref="null"/>.</param>
		public void PushBack(T item) => _list.PushFront(item);

		/// <summary>
		/// Добавляет объект в начало очереди <see cref="Deque{T}"/>.
		/// </summary>
		/// <param name="item">Объект, добавляемый в начало очереди <see cref="Deque{T}"/>. Для ссылочных типов допускается значение <see cref="null"/>.</param>
		public void PushFront(T item) => _list.PushBack(item);

		/// <summary>
		/// Удаляет объект из конца очереди <see cref="Deque{T}"/> и возвращает его.
		/// </summary>
		/// <returns>Объект, удаляемый из конца очереди <see cref="Deque{T}"/>.</returns>
		/// <exception cref="InvalidOperationException">Очередь <see cref="Deque{T}"/> является пустой.</exception>
		public T PopBack()
		{
			if (_list.Count <= 0)
				throw new InvalidOperationException("Очередь пуста.");
			var result = _list._head.Value;
			_list.Remove(result);
			return result;
		}

		/// <summary>
		/// Удаляет объект из начала очереди <see cref="Deque{T}"/> и возвращает его.
		/// </summary>
		/// <returns>Объект, удаляемый из начала очереди <see cref="Deque{T}"/>.</returns>
		/// <exception cref="InvalidOperationException">Очередь <see cref="Deque{T}"/> является пустой.</exception>
		public T PopFront()
		{
			if (_list.Count <= 0)
				throw new InvalidOperationException("Очередь пуста.");
			var result = _list._tail.Value;
			_list.Remove(result);
			return result;
		}

		/// <summary>
		/// Возвращает объект, находящийся в начале очереди <see cref="Deque{T}"/>, но не удаляет его.
		/// </summary>
		/// <returns>Объект, находящийся в начале очереди <see cref="Deque{T}"/>.</returns>
		/// <exception cref="InvalidOperationException">Очередь <see cref="Deque{T}"/> является пустой.</exception>
		public T PeekFront()
		{
			if (_list.Count <= 0)
				throw new InvalidOperationException("Очередь пуста.");
			return _list._head.Value;
		}

		/// <summary>
		/// Возвращает объект, находящийся в конце очереди <see cref="Deque{T}"/>, но не удаляет его.
		/// </summary>
		/// <returns>Объект, находящийся в конце очереди <see cref="Deque{T}"/>.</returns>
		/// <exception cref="InvalidOperationException">Очередь <see cref="Deque{T}"/> является пустой.</exception>
		public T PeekBack()
		{
			if (_list.Count <= 0)
				throw new InvalidOperationException("Очередь пуста.");
			return _list._tail.Value;
		}

		/// <summary>
		/// Добавляет объект в конец очереди <see cref="Deque{T}"/>.
		/// </summary>
		/// <param name="item">Объект, добавляемый в конец очереди <see cref="Deque{T}"/>. Для ссылочных типов допускается значение <see cref="null"/>.</param>
		void ICollection<T>.Add(T item) => PushBack(item);

		/// <summary>
		/// Удаляет все элементы двусторонней очереди <see cref="Deque{T}"/>.
		/// </summary>
		public void Clear() => _list.Clear();

		/// <summary>
		/// Создает неполную копию <see cref="Deque{T}"/>.
		/// </summary>
		/// <returns>Неполная копия <see cref="Deque{T}"/>.</returns>
		public object Clone() => new Deque<T>(this);

		/// <exception cref="NotSupportedException">Операция не поддерживается данным типом коллекций.</exception>
		public bool Contains(T item) => throw new NotSupportedException();

		/// <summary>
		/// Копирует <see cref="Deque{T}"/> целиком в совместимый одномерный массив, начиная с указанного индекса конечного массива.
		/// </summary>
		/// <param name="array">Одномерный массив <see cref="Array"/>, в который копируются элементы из двусторонней очереди <see cref="Deque{T}"/>. Массив <see cref="Array"/> должен иметь индексацию, начинающуюся с нуля.</param>
		/// <param name="arrayIndex">Отсчитываемый от нуля индекс в массиве array, указывающий начало копирования.</param>
		/// <exception cref="ArgumentNullException">Свойство <paramref name="array"/> имеет значение <see cref="null"/>.</exception>
		/// <exception cref="ArgumentOutOfRangeException">Значение параметра <paramref name="arrayIndex"/> меньше 0.</exception>
		/// <exception cref="ArgumentException">Число элементов в исходной двусторонней очереди <see cref="Deque{T}"/> больше доступного места от положения, заданного значением параметра <paramref name="arrayIndex"/>, до конца массива назначения <paramref name="array"/>.</exception>
		public void CopyTo(T[] array, int arrayIndex) => _list.CopyTo(array, arrayIndex);

		/// <summary>
		/// Копирует <see cref="Deque{T}"/> целиком в совместимый одномерный массив, начиная с указанного индекса конечного массива.
		/// </summary>
		/// <param name="array">Одномерный массив <see cref="Array"/>, в который копируются элементы из двусторонней очереди <see cref="Deque{T}"/>. Массив <see cref="Array"/> должен иметь индексацию, начинающуюся с нуля.</param>
		/// <param name="index">Отсчитываемый от нуля индекс в массиве array, указывающий начало копирования.</param>
		/// <exception cref="ArgumentNullException">Свойство <paramref name="array"/> имеет значение <see cref="null"/>.</exception>
		/// <exception cref="ArgumentOutOfRangeException">Значение параметра <paramref name="index"/> меньше 0.</exception>
		/// <exception cref="ArgumentException">Число элементов в исходной двусторонней очереди <see cref="Deque{T}"/> больше доступного места от положения, заданного значением параметра <paramref name="index"/>, до конца массива назначения <paramref name="array"/>.</exception>
		public void CopyTo(Array array, int index) => _list.CopyTo(array, index);

		/// <exception cref="NotSupportedException">Операция не поддерживается данным типом коллекций.</exception>
		bool ICollection<T>.Remove(T item) => throw new NotSupportedException();

		/// <summary>
		/// Возвращает перечислитель, осуществляющий перебор элементов двусторонней очереди <see cref="Deque{T}"/>.
		/// </summary>
		/// <returns>Новый объект <see cref="IEnumerator{T}"/> для <see cref="Deque{T}"/>.</returns>
		IEnumerator<T> IEnumerable<T>.GetEnumerator() => _list.GetEnumerator();

		/// <summary>
		/// Возвращает перечислитель, осуществляющий перебор элементов двусторонней очереди <see cref="Deque{T}"/>.
		/// </summary>
		/// <returns>Новый объект <see cref="IEnumerator{T}"/> для <see cref="Deque{T}"/>.</returns>
		IEnumerator IEnumerable.GetEnumerator() => _list.GetEnumerator();

		#endregion

	}
}
