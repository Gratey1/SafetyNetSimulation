using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class SKState<T>
{
		protected int _mecanimStateHash;
		protected SKStateMachine<T> machine;
		protected T context;
		
		public SKState ()
		{
		}


		/// <summary>
		/// constructor that takes the mecanim state name as a string
		/// </summary>
		public SKState (string mecanimStateName) : this( Animator.StringToHash( mecanimStateName ) )
		{
		}
		
		
		/// <summary>
		/// constructor that takes the mecanim state hash
		/// </summary>
		public SKState (int mecanimStateHash)
		{
			_mecanimStateHash = mecanimStateHash;
		}
		
		internal void SetMachineAndContext (SKStateMachine<T> _machine, T _context)
		{
			machine = _machine;
			context = _context;
			OnInitialized ();
		}


		/// <summary>
		/// called directly after the machine and context are set allowing the state to do any required setup
		/// </summary>
		public virtual void OnInitialized ()
		{
		}

		public virtual void Begin ()
		{
		}
		
		public abstract void Update (float deltaTime);
		
		public virtual void End ()
		{
		}
	
}
