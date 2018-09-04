using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Text;

namespace ASDC.Collections
{
	/// <summary>
	/// Представляет строго типизированный стек объектов, основанный на принципе "последним поступил — первым обслужен (LIFO), который выдает уведомления при добавлении и удалении элементов, а также при обновлении стека.
	/// </summary>
	/// <typeparam name="T">Тип элементов стека.</typeparam>
	public class ObservableStack<T> : Stack<T>, INotifyCollectionChanged, INotifyPropertyChanged
	{

		#region ~Constructors~

		/// <summary>
		/// Инициализирует новый экземпляр класса <see cref="ObservableStack{T}"/>, который является пустым.
		/// </summary>
		public ObservableStack() : base() { }

		/// <summary>
		/// Инициализирует новый экземпляр <see cref="ObservableStack{T}"/>, который содержит элементы, скопированные из указанной коллекции.
		/// </summary>
		/// <param name="collection">Последовательность, элементы которой копируются в новый стек.</param>
		/// <exception cref="ArgumentNullException">Свойство <paramref name="collection"/> имеет значение <see cref="null"/>.</exception>
		public ObservableStack(IEnumerable<T> collection) : base(collection) { }

		#endregion

		#region ~Methods~

		/// <summary>
		/// Вставляет объект как верхний элемент стека <see cref="ObservableStack{T}"/>.
		/// </summary>
		/// <param name="item">Объект, вставляемый в <see cref="ObservableStack{T}"/>. Для ссылочных типов допускается значение null.</param>
		public new void Push(T item)
		{
			base.Push(item);
			CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, 0));
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Count)));
		}

		/// <summary>
		/// Удаляет и возвращает объект в начале <see cref="ObservableStack{T}"/>.
		/// </summary>
		/// <returns>Объект, удаляемый из начала <see cref="ObservableStack{T}"/>.</returns>
		/// <exception cref="InvalidOperationException">Стек <see cref="Stack{T}"/> является пустым.</exception>
		public new T Pop()
		{
			var result = base.Pop();
			CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, result, 0));
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Count)));
			return result;
		}

		/// <summary>
		/// Удаляет все элементы стека <see cref="ObservableStack{T}"/>.
		/// </summary>
		public new void Clear()
		{
			base.Clear();
			CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Count)));
		}

		#endregion

		#region ~Events~

		/// <summary>
		/// Событие происходит при изменении очереди.
		/// </summary>
		public event NotifyCollectionChangedEventHandler CollectionChanged;

		/// <summary>
		/// Событие происходит при изменении значения свойства.
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		#endregion

	}
}
