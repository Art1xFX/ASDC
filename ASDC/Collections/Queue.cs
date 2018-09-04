using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace ASDC.Collections
{
	/// <summary>
	/// Представляет очередь объектов, основанную на принципе "первым поступил — первым обслужен (FIFO)".
	/// </summary>
	/// <typeparam name="T">Тип элементов в очереди.</typeparam>
	public class Queue<T> : ICollection<T>, ICollection, ICloneable, IReadOnlyCollection<T>
	{
		
		#region ~Fields~

		// Внутренний односвязный список, на котором основывается очередь
		SinglyLinkedList<T> _list;

		#endregion
		
		#region ~Constructors~

		/// <summary>
		/// Инициализирует новый экземпляр класса <see cref="Queue{T}"/>, который является пустым.
		/// </summary>
		public Queue()
		{
			_list = new SinglyLinkedList<T>();
		}

		/// <summary>
		/// Инициализирует новый экземпляр <see cref="Queue{T}"/>, который содержит элементы, скопированные из указанной коллекции.
		/// </summary>
		/// <param name="collection">Коллекция, элементы которой копируются в очередь.</param>
		/// <exception cref="ArgumentNullException">Свойство <paramref name="collection"/> имеет значение <see cref="null"/>.</exception>
		public Queue(IEnumerable<T> collection)
		{
			if (collection == null)
				throw new ArgumentNullException(nameof(collection));
			_list = new SinglyLinkedList<T>(collection);
		}

		#endregion

		#region ~Properties~

		/// <summary>
		/// Получает число элементов, содержащихся в очереди <see cref="Queue{T}"/>.
		/// </summary>
		public int Count => _list.Count;

		/// <summary>
		/// Получает значение, указывающее, является ли объект <see cref="Queue{T}"/> доступным только для чтения.
		/// </summary>
		/// <value>Значение <see cref="true"/>, если объект <see cref="Queue{T}"/> доступен только для чтения; в противном случае — значение <see cref="false"/>.</value>
		public bool IsReadOnly => false;

		/// <summary>
		/// Получает объект, с помощью которого можно синхронизировать доступ к очереди <see cref="Queue{T}"/>.
		/// </summary>
		/// <value>Объект <see cref="object"/>, который может использоваться для синхронизации доступа к односвязному списку <see cref="Queue{T}"/>.</value>
		public object SyncRoot => _list.SyncRoot;

		/// <summary>
		/// Возвращает значение, показывающее, является ли доступ к очереди <see cref="Queue{T}"/> синхронизированным (потокобезопасным).
		/// </summary>
		/// <value><see cref="true"/>, если доступ к <see cref="Queue{T}"/> является синхронизированным (потокобезопасным); в противном случае — <see cref="false"/>. Значение по умолчанию — <see cref="false"/>.</value>
		public bool IsSynchronized => _list.IsSynchronized;

		#endregion

		#region ~Methods~

		/// <summary>
		/// Добавляет объект в конец очереди <see cref="Queue{T}"/>.
		/// </summary>
		/// <param name="item">Объект, добавляемый в очередь <see cref="Queue{T}"/>. Для ссылочных типов допускается значение <see cref="null"/>.</param>

		void ICollection<T>.Add(T item) => Enqueue(item);

		/// <summary>
		/// Добавляет объект в конец очереди <see cref="Queue{T}"/>.
		/// </summary>
		/// <param name="item">Объект, добавляемый в очередь <see cref="Queue{T}"/>. Для ссылочных типов допускается значение <see cref="null"/>.</param>
		public void Enqueue(T item)
		{
			_list.Add(item);
		}

		/// <summary>
		/// Удаляет объект из начала очереди <see cref="Queue{T}"/> и возвращает его.
		/// </summary>
		/// <returns>Объект, удаляемый из начала очереди <see cref="Queue{T}"/>.</returns>
		/// <exception cref="InvalidOperationException">Очередь <see cref="Queue{T}"/> является пустой.</exception>
		public T Dequeue()
		{
			if (_list.Count <= 0)
				throw new InvalidOperationException("Очередь пуста.");
			var result = _list.First();
			_list.Remove(_list.First());
			return result;
		}

		/// <summary>
		/// Возвращает объект, находящийся в начале очереди <see cref="Queue{T}"/>, но не удаляет его.
		/// </summary>
		/// <returns>Объект, находящийся в начале очереди <see cref="Queue{T}"/>.</returns>
		/// <exception cref="InvalidOperationException">Очередь <see cref="Queue{T}"/> является пустой.</exception>
		public T Peek()
		{
			if (_list.Count <= 0)
				throw new InvalidOperationException("Очередь пуста.");
			return _list.First();
		}
		
		/// <summary>
		/// Удаляет все элементы очереди <see cref="Queue{T}"/>.
		/// </summary>
		public void Clear() => _list.Clear();

		/// <summary>
		/// Создает неполную копию <see cref="Queue{T}"/>.
		/// </summary>
		/// <returns>Неполная копия <see cref="Queue{T}"/>.</returns>
		public object Clone()
		{
			return new Queue<T>(_list);
		}

		/// <exception cref="NotSupportedException">Операция не поддерживается данным типом коллекций.</exception>
		public bool Contains(T item) => throw new NotSupportedException();

		/// <summary>
		/// Копирует <see cref="Queue{T}"/> целиком в совместимый одномерный массив, начиная с указанного индекса конечного массива.
		/// </summary>
		/// <param name="array">Одномерный массив <see cref="Array"/>, в который копируются элементы из очереди <see cref="Queue{T}"/>. Массив <see cref="Array"/> должен иметь индексацию, начинающуюся с нуля.</param>
		/// <param name="arrayIndex">Отсчитываемый от нуля индекс в массиве array, указывающий начало копирования.</param>
		/// <exception cref="ArgumentNullException">Свойство <paramref name="array"/> имеет значение <see cref="null"/>.</exception>
		/// <exception cref="ArgumentOutOfRangeException">Значение параметра <paramref name="arrayIndex"/> меньше 0.</exception>
		/// <exception cref="ArgumentException">Число элементов в исходной очереди <see cref="Queue{T}"/> больше доступного места от положения, заданного значением параметра <paramref name="arrayIndex"/>, до конца массива назначения <paramref name="array"/>.</exception>
		public void CopyTo(T[] array, int arrayIndex) => _list.CopyTo(array, arrayIndex);

		/// <summary>
		/// Копирует <see cref="Queue{T}"/> целиком в совместимый одномерный массив, начиная с указанного индекса конечного массива.
		/// </summary>
		/// <param name="array">Одномерный массив <see cref="Array"/>, в который копируются элементы из очереди <see cref="Queue{T}"/>. Массив <see cref="Array"/> должен иметь индексацию, начинающуюся с нуля.</param>
		/// <param name="index">Отсчитываемый от нуля индекс в массиве array, указывающий начало копирования.</param>
		/// <exception cref="ArgumentNullException">Свойство <paramref name="array"/> имеет значение <see cref="null"/>.</exception>
		/// <exception cref="ArgumentOutOfRangeException">Значение параметра <paramref name="index"/> меньше 0.</exception>
		/// <exception cref="ArgumentException">Число элементов в исходной очереди <see cref="Queue{T}"/> больше доступного места от положения, заданного значением параметра <paramref name="index"/>, до конца массива назначения <paramref name="array"/>.</exception>
		public void CopyTo(Array array, int index) => _list.CopyTo(array, index);

		/// <exception cref="NotSupportedException">Операция не поддерживается данным типом коллекций.</exception>
		bool ICollection<T>.Remove(T item) => throw new NotSupportedException();

		/// <summary>
		/// Возвращает перечислитель, осуществляющий перебор элементов очереди <see cref="Queue{T}"/>.
		/// </summary>
		/// <returns>Новый объект <see cref="IEnumerator{T}"/> для <see cref="Queue{T}"/>.</returns>
		public IEnumerator<T> GetEnumerator() => _list.GetEnumerator();

		/// <summary>
		/// Возвращает перечислитель, осуществляющий перебор элементов очереди <see cref="Queue{T}"/>.
		/// </summary>
		/// <returns>Новый объект <see cref="IEnumerator{T}"/> для <see cref="Queue{T}"/>.</returns>
		IEnumerator IEnumerable.GetEnumerator() => _list.GetEnumerator();

		#endregion

	}
}
