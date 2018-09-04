using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ASDC.Collections
{
	/// <summary>
	/// Представляет строго типизированный стек объектов, основанный на принципе "последним поступил — первым обслужен (LIFO).
	/// </summary>
	/// <typeparam name="T">Тип элементов стека.</typeparam>
	public class Stack<T> : ICollection<T>, ICollection, ICloneable, IReadOnlyCollection<T>
	{

		#region ~Fields~

		// Двусвязный список, на котором основывается стек
		DoublyLinkedList<T> _list;

		#endregion

		#region ~Constructors~

		/// <summary>
		/// Инициализирует новый экземпляр класса <see cref="Stack{T}"/>, который является пустым.
		/// </summary>
		public Stack()
		{
			_list = new DoublyLinkedList<T>();
		}

		/// <summary>
		/// Инициализирует новый экземпляр <see cref="Stack{T}"/>, который содержит элементы, скопированные из указанной коллекции.
		/// </summary>
		/// <param name="collection">Последовательность, элементы которой копируются в новый стек.</param>
		/// <exception cref="ArgumentNullException">Свойство <paramref name="collection"/> имеет значение <see cref="null"/>.</exception>
		public Stack(IEnumerable<T> collection)
		{
			_list = new DoublyLinkedList<T>(collection);
		}

		#endregion

		#region ~Properties~

		/// <summary>
		/// Получает число элементов, содержащихся в стеке <see cref="Stack{T}"/>.
		/// </summary>
		public int Count => _list.Count;

		/// <summary>
		/// Получает значение, указывающее, является ли объект <see cref="Stack{T}"/> доступным только для чтения.
		/// </summary>
		/// <value>Значение <see cref="true"/>, если объект <see cref="Stack{T}"/> доступен только для чтения; в противном случае — значение <see cref="false"/>.</value>
		public bool IsReadOnly => false;

		/// <summary>
		/// Получает объект, с помощью которого можно синхронизировать доступ к стеку <see cref="Stack{T}"/>.
		/// </summary>
		/// <value>Объект <see cref="object"/>, который может использоваться для синхронизации доступа к стеку <see cref="Stack{T}"/>.</value>
		public object SyncRoot => _list.SyncRoot;

		/// <summary>
		/// Возвращает значение, показывающее, является ли доступ к стеку <see cref="Stack{T}"/> синхронизированным (потокобезопасным).
		/// </summary>
		/// <value><see cref="true"/>, если доступ к <see cref="Stack{T}"/> является синхронизированным (потокобезопасным); в противном случае — <see cref="false"/>. Значение по умолчанию — <see cref="false"/>.</value>
		public bool IsSynchronized => _list.IsSynchronized;

		#endregion

		#region ~Methods~

		/// <summary>
		/// Вставляет объект как верхний элемент стека <see cref="Stack{T}"/>.
		/// </summary>
		/// <param name="item">Объект, вставляемый в <see cref="Stack{T}"/>. Для ссылочных типов допускается значение null.</param>
		public void Push(T item)
		{
			_list.PushFront(item);
		}

		/// <summary>
		/// Удаляет и возвращает объект в начале <see cref="Stack{T}"/>.
		/// </summary>
		/// <returns>Объект, удаляемый из начала <see cref="Stack{T}"/>.</returns>
		/// <exception cref="InvalidOperationException">Стек <see cref="Stack{T}"/> является пустым.</exception>
		public T Pop()
		{
			if (_list.Count <= 0)
				throw new InvalidOperationException("Стек пуст.");
			var result = _list.First();
			_list.Remove(_list.First());
			return result;
		}

		/// <summary>
		/// Возвращает объект в начале стека <see cref="Stack{T}"/>, но не удаляет его.
		/// </summary>
		/// <returns>Объект, находящийся в начале стека <see cref="Stack{T}"/>.</returns>
		/// <exception cref="InvalidOperationException">Стек <see cref="Stack{T}"/> является пустым.</exception>
		public T Peek()
		{
			if (_list.Count <= 0)
				throw new InvalidOperationException("Стек пуст.");
			return _list.First();
		}

		/// <summary>
		/// Удаляет все элементы стека <see cref="Stack{T}"/>.
		/// </summary>
		public void Clear() => _list.Clear();

		/// <summary>
		/// Создает неполную копию <see cref="Stack{T}"/>.
		/// </summary>
		/// <returns>Неполная копия <see cref="Stack{T}"/>.</returns>
		public object Clone()
		{
			return new Stack<T>(_list);
		}

		/// <summary>
		/// Вставляет объект как верхний элемент стека <see cref="Stack{T}"/>.
		/// </summary>
		/// <param name="item">Объект, вставляемый в <see cref="Stack{T}"/>. Для ссылочных типов допускается значение null.</param>
		void ICollection<T>.Add(T item) => Push(item);

		/// <exception cref="NotSupportedException">Операция не поддерживается данным типом коллекций.</exception>
		public bool Contains(T item) => throw new NotSupportedException();

		/// <summary>
		/// Копирует <see cref="Stack{T}"/> целиком в совместимый одномерный массив, начиная с указанного индекса конечного массива.
		/// </summary>
		/// <param name="array">Одномерный массив <see cref="Array"/>, в который копируются элементы из стека <see cref="Stack{T}"/>. Массив <see cref="Array"/> должен иметь индексацию, начинающуюся с нуля.</param>
		/// <param name="arrayIndex">Отсчитываемый от нуля индекс в массиве array, указывающий начало копирования.</param>
		/// <exception cref="ArgumentNullException">Свойство <paramref name="array"/> имеет значение <see cref="null"/>.</exception>
		/// <exception cref="ArgumentOutOfRangeException">Значение параметра <paramref name="arrayIndex"/> меньше 0.</exception>
		/// <exception cref="ArgumentException">Число элементов в исходном стеке <see cref="Stack{T}"/> больше доступного места от положения, заданного значением параметра <paramref name="arrayIndex"/>, до конца массива назначения <paramref name="array"/>.</exception>
		public void CopyTo(T[] array, int arrayIndex) => _list.CopyTo(array, arrayIndex);

		/// <summary>
		/// Копирует <see cref="Stack{T}"/> целиком в совместимый одномерный массив, начиная с указанного индекса конечного массива.
		/// </summary>
		/// <param name="array">Одномерный массив <see cref="Array"/>, в который копируются элементы из стека <see cref="Stack{T}"/>. Массив <see cref="Array"/> должен иметь индексацию, начинающуюся с нуля.</param>
		/// <param name="index">Отсчитываемый от нуля индекс в массиве array, указывающий начало копирования.</param>
		/// <exception cref="ArgumentNullException">Свойство <paramref name="array"/> имеет значение <see cref="null"/>.</exception>
		/// <exception cref="ArgumentOutOfRangeException">Значение параметра <paramref name="index"/> меньше 0.</exception>
		/// <exception cref="ArgumentException">Число элементов в исходном стеке <see cref="Stack{T}"/> больше доступного места от положения, заданного значением параметра <paramref name="index"/>, до конца массива назначения <paramref name="array"/>.</exception>
		public void CopyTo(Array array, int index) => _list.CopyTo(array, index);

		/// <exception cref="NotSupportedException">Операция не поддерживается данным типом коллекций.</exception>
		bool ICollection<T>.Remove(T item) => throw new NotSupportedException();

		/// <summary>
		/// Возвращает перечислитель, осуществляющий перебор элементов стека <see cref="Stack{T}"/>.
		/// </summary>
		/// <returns>Новый объект <see cref="IEnumerator"/> для <see cref="Stack{T}"/>.</returns>
		public IEnumerator<T> GetEnumerator() => _list.GetEnumerator();

		/// <summary>
		/// Возвращает перечислитель, осуществляющий перебор элементов стека <see cref="Stack{T}"/>.
		/// </summary>
		/// <returns>Новый объект <see cref="IEnumerator"/> для <see cref="Stack{T}"/>.</returns>
		IEnumerator IEnumerable.GetEnumerator() => _list.GetEnumerator();
				
		#endregion

		
	}
}
