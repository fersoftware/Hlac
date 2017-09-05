using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Busca.Util {
	// ATENÇÃO!!! Por questões de desempenho, um PriorityQueue (Heap)
	// não armazena os elementos no vetor de forma TOTALMENTE ordenada!
	// O primeiro elemento é certo ser o menor de todos (ou maior...)
	// Ao se remover o primeiro elemento, então arruma-se o vetor de modo
	// que o novo elemento passe a ser o menor (ou maior) elemento do vetor
	//
	// esse código foi convertido copm base na classe original em Java:
	// http://grepcode.com/file/repository.grepcode.com/java/root/jdk/openjdk/6-b14/java/util/PriorityQueue.java
	public class PriorityQueue<E> where E : class {
		/*
		 * Copyright 2003-2006 Sun Microsystems, Inc.  All Rights Reserved.
		 * DO NOT ALTER OR REMOVE COPYRIGHT NOTICES OR THIS FILE HEADER.
		 *
		 * This code is free software; you can redistribute it and/or modify it
		 * under the terms of the GNU General Public License version 2 only, as
		 * published by the Free Software Foundation.  Sun designates this
		 * particular file as subject to the "Classpath" exception as provided
		 * by Sun in the LICENSE file that accompanied this code.
		 *
		 * This code is distributed in the hope that it will be useful, but WITHOUT
		 * ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
		 * FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License
		 * version 2 for more details (a copy is included in the LICENSE file that
		 * accompanied this code).
		 *
		 * You should have received a copy of the GNU General Public License version
		 * 2 along with this work; if not, write to the Free Software Foundation,
		 * Inc., 51 Franklin St, Fifth Floor, Boston, MA 02110-1301 USA.
		 *
		 * Please contact Sun Microsystems, Inc., 4150 Network Circle, Santa Clara,
		 * CA 95054 USA or visit www.sun.com if you need additional information or
		 * have any questions.
		 */

		private const int DEFAULT_INITIAL_CAPACITY = 11;

		/*
		 * Priority queue represented as a balanced binary heap: the two
		 * children of queue[n] are queue[2*n+1] and queue[2*(n+1)].  The
		 * priority queue is ordered by comparator, or by the elements'
		 * natural ordering, if comparator is null: For each node n in the
		 * heap and each descendant d of n, n <= d.  The element with the
		 * lowest value is in queue[0], assuming the queue is nonempty.
		 */
		private E[] queue;

		/*
		 * The number of elements in the priority queue.
		 */
		private int size = 0;

		/*
		 * The comparator, or null if priority queue uses elements'
		 * natural ordering.
		 */
		private IComparer<E> comparer;

		/*
		 * The number of times this priority queue has been
		 * <i>structurally modified</i>.  See AbstractList for gory details.
		 */
		private int modCount = 0;

		/*
		 * Creates a {@code PriorityQueue} with the default initial
		 * capacity (11) that orders its elements according to their
		 * {@linkplain Comparable natural ordering}.
		 */
		public PriorityQueue()
			: this(DEFAULT_INITIAL_CAPACITY, null) {
		}

		/*
		 * Creates a {@code PriorityQueue} with the specified initial
		 * capacity that orders its elements according to their
		 * {@linkplain Comparable natural ordering}.
		 *
		 * @param initialCapacity the initial capacity for this priority queue
		 * @throws IllegalArgumentException if {@code initialCapacity} is less
		 *         than 1
		 */
		public PriorityQueue(int initialCapacity)
			: this(initialCapacity, null) {
		}

		/*
		 * Creates a {@code PriorityQueue} with the specified initial capacity
		 * that orders its elements according to the specified comparator.
		 *
		 * @param  initialCapacity the initial capacity for this priority queue
		 * @param  comparator the comparator that will be used to order this
		 *         priority queue.  If {@code null}, the {@linkplain Comparable
		 *         natural ordering} of the elements will be used.
		 * @throws IllegalArgumentException if {@code initialCapacity} is
		 *         less than 1
		 */
		public PriorityQueue(int initialCapacity, IComparer<E> comparer) {
			queue = new E[initialCapacity];
			this.comparer = comparer;
		}

		/*
		 * Increases the capacity of the array.
		 *
		 * @param minCapacity the desired minimum capacity
		 */
		private void Grow(int minCapacity) {
			int oldCapacity = queue.Length;
			// Double size if small; else grow by 50%
			int newCapacity = ((oldCapacity < 64) ?
							   ((oldCapacity + 1) * 2) :
							   ((oldCapacity / 2) * 3));
			if (newCapacity < 0) // overflow
				newCapacity = int.MaxValue;
			if (newCapacity < minCapacity)
				newCapacity = minCapacity;
			Array.Resize<E>(ref queue, newCapacity);
		}

		/*
		 * Inserts the specified element into this priority queue.
		 *
		 * @return {@code true} (as specified by {@link Collection#add})
		 * @throws ClassCastException if the specified element cannot be
		 *         compared with elements currently in this priority queue
		 *         according to the priority queue's ordering
		 * @throws NullPointerException if the specified element is null
		 */
		public bool Add(E e) {
			return Offer(e);
		}

		/*
		 * Inserts the specified element into this priority queue.
		 *
		 * @return {@code true} (as specified by {@link Queue#offer})
		 * @throws ClassCastException if the specified element cannot be
		 *         compared with elements currently in this priority queue
		 *         according to the priority queue's ordering
		 * @throws NullPointerException if the specified element is null
		 */
		public bool Offer(E e) {
			if (e == null)
				throw new NullReferenceException();
			modCount++;
			int i = size;
			if (i >= queue.Length)
				Grow(i + 1);
			size = i + 1;
			if (i == 0)
				queue[0] = e;
			else
				SiftUp(i, e);
			return true;
		}

		public E Peek() {
			if (size == 0)
				return null;
			return queue[0];
		}

		private int IndexOf(E o) {
			if (o != null) {
				for (int i = 0; i < size; i++)
					if (o.Equals(queue[i]))
						return i;
			}
			return -1;
		}

		/*
		 * Retrieves and removes the head of this queue.  This method differs
		 * from {@link #poll poll} only in that it throws an exception if this
		 * queue is empty.
		 *
		 * <p>This implementation returns the result of <tt>poll</tt>
		 * unless the queue is empty.
		 *
		 * @return the head of this queue
		 * @throws NoSuchElementException if this queue is empty
		 */
		public E Remove() {
			E x = Poll();
			if (x != null)
				return x;
			else
				throw new IndexOutOfRangeException();
		}

		/*
		 * Removes a single instance of the specified element from this queue,
		 * if it is present.  More formally, removes an element {@code e} such
		 * that {@code o.equals(e)}, if this queue contains one or more such
		 * elements.  Returns {@code true} if and only if this queue contained
		 * the specified element (or equivalently, if this queue changed as a
		 * result of the call).
		 *
		 * @param o element to be removed from this queue, if present
		 * @return {@code true} if this queue changed as a result of the call
		 */
		public bool Remove(E o) {
			int i = IndexOf(o);
			if (i == -1) {
				return false;
			} else {
				RemoveAt(i);
				return true;
			}
		}

		/*
		 * Version of remove using reference equality, not equals.
		 * Needed by iterator.remove.
		 *
		 * @param o element to be removed from this queue, if present
		 * @return {@code true} if removed
		 */
		private bool RemoveEq(E o) {
			for (int i = 0; i < size; i++) {
				if (o == queue[i]) {
					RemoveAt(i);
					return true;
				}
			}
			return false;
		}

		/*
		 * Returns {@code true} if this queue contains the specified element.
		 * More formally, returns {@code true} if and only if this queue contains
		 * at least one element {@code e} such that {@code o.equals(e)}.
		 *
		 * @param o object to be checked for containment in this queue
		 * @return {@code true} if this queue contains the specified element
		 */
		public bool Contains(E o) {
			return IndexOf(o) != -1;
		}

		/*
		 * Returns an array containing all of the elements in this queue.
		 * The elements are in no particular order.
		 *
		 * <p>The returned array will be "safe" in that no references to it are
		 * maintained by this queue.  (In other words, this method must allocate
		 * a new array).  The caller is thus free to modify the returned array.
		 *
		 * <p>This method acts as bridge between array-based and collection-based
		 * APIs.
		 *
		 * @return an array containing all of the elements in this queue
		 */
		public E[] ToArray() {
			E[] c = new E[size];
			Array.Copy(queue, c, size);
			return c;
		}

		public int Count {
			get {
				return size;
			}
		}

		/*
		 * Removes all of the elements from this priority queue.
		 * The queue will be empty after this call returns.
		 */
		public void Clear() {
			modCount++;
			for (int i = 0; i < size; i++)
				queue[i] = null;
			size = 0;
		}

		public E Poll() {
			if (size == 0)
				return null;
			int s = --size;
			modCount++;
			E result = queue[0];
			E x = queue[s];
			queue[s] = null;
			if (s != 0)
				SiftDown(0, x);
			return result;
		}

		/*
		 * Removes the ith element from queue.
		 *
		 * Normally this method leaves the elements at up to i-1,
		 * inclusive, untouched.  Under these circumstances, it returns
		 * null.  Occasionally, in order to maintain the heap invariant,
		 * it must swap a later element of the list with one earlier than
		 * i.  Under these circumstances, this method returns the element
		 * that was previously at the end of the list and is now at some
		 * position before i. This fact is used by iterator.remove so as to
		 * avoid missing traversing elements.
		 */
		private E RemoveAt(int i) {
			if (i < 0 || i >= size)
				throw new IndexOutOfRangeException();
			modCount++;
			int s = --size;
			if (s == i) // removed last element
				queue[i] = null;
			else {
				E moved = queue[s];
				queue[s] = null;
				SiftDown(i, moved);
				if (queue[i] == moved) {
					SiftUp(i, moved);
					if (queue[i] != moved)
						return moved;
				}
			}
			return null;
		}

		/*
		 * Inserts item x at position k, maintaining heap invariant by
		 * promoting x up the tree until it is greater than or equal to
		 * its parent, or is the root.
		 *
		 * To simplify and speed up coercions and comparisons. the
		 * Comparable and Comparator versions are separated into different
		 * methods that are otherwise identical. (Similarly for siftDown.)
		 *
		 * @param k the position to fill
		 * @param x the item to insert
		 */
		private void SiftUp(int k, E x) {
			if (comparer != null)
				SiftUpUsingComparator(k, x);
			else
				SiftUpComparable(k, x);
		}

		private void SiftUpComparable(int k, E x) {
			IComparable<E> key = (IComparable<E>)x;
			while (k > 0) {
				int parent = (k - 1) >> 1;
				E e = queue[parent];
				if (key.CompareTo(e) >= 0)
					break;
				queue[k] = e;
				k = parent;
			}
			queue[k] = x;
		}

		private void SiftUpUsingComparator(int k, E x) {
			while (k > 0) {
				int parent = (k - 1) >> 1;
				E e = queue[parent];
				if (comparer.Compare(x, e) >= 0)
					break;
				queue[k] = e;
				k = parent;
			}
			queue[k] = x;
		}

		/*
		 * Inserts item x at position k, maintaining heap invariant by
		 * demoting x down the tree repeatedly until it is less than or
		 * equal to its children or is a leaf.
		 *
		 * @param k the position to fill
		 * @param x the item to insert
		 */
		private void SiftDown(int k, E x) {
			if (comparer != null)
				SiftDownUsingComparator(k, x);
			else
				SiftDownComparable(k, x);
		}

		private void SiftDownComparable(int k, E x) {
			IComparable<E> key = (IComparable<E>)x;
			int half = size >> 1; // loop while a non-leaf
			while (k < half) {
				int child = (k << 1) + 1; // assume left child is least
				E c = queue[child];
				int right = child + 1;
				if (right < size &&
					((IComparable<E>)c).CompareTo(queue[right]) > 0)
                c = queue[child = right];
				if (key.CompareTo(c) <= 0)
					break;
				queue[k] = c;
				k = child;
			}
			queue[k] = x;
		}

		private void SiftDownUsingComparator(int k, E x) {
			int half = size >> 1;
			while (k < half) {
				int child = (k << 1) + 1;
				E c = queue[child];
				int right = child + 1;
				if (right < size &&
					comparer.Compare(c, queue[right]) > 0)
					c = queue[child = right];
				if (comparer.Compare(x, c) <= 0)
					break;
				queue[k] = c;
				k = child;
			}
			queue[k] = x;
		}

		/*
		 * Establishes the heap invariant (described above) in the entire tree,
		 * assuming nothing about the order of the elements prior to the call.
		 */
		private void Heapify() {
			for (int i = (size >> 1) - 1; i >= 0; i--)
				SiftDown(i, (E)queue[i]);
		}

		/*
		 * Returns the comparator used to order the elements in this
		 * queue, or {@code null} if this queue is sorted according to
		 * the {@linkplain Comparable natural ordering} of its elements.
		 *
		 * @return the comparator used to order this queue, or
		 *         {@code null} if this queue is sorted according to the
		 *         natural ordering of its elements
		 */
		public IComparer<E> Comparer {
			get {
				return comparer;
			}
		}
	}
}
