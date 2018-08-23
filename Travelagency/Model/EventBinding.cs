﻿using System;
using System.Reflection;
using System.Windows;
using System.Windows.Input;

namespace Travelagency.Model
{
    /// <summary>
    /// Provides a binding mechanism between a named event and a command.
    /// </summary>
    public class EventBinding : Freezable
    {
        /// <summary>
        /// When implemented in a derived class, creates a new instance of the <see cref="T:System.Windows.Freezable"/> derived class.
        /// </summary>
        /// <returns>
        /// The new instance.
        /// </returns>
        protected override Freezable CreateInstanceCore()
        {
            return new EventBinding();
        }

        /// <summary>
        /// The EventName Dependency Property.
        /// </summary>
        private static readonly DependencyProperty EventNameProperty =
          DependencyProperty.Register("EventName", typeof(string), typeof(EventBinding),
          new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets the name of the event.
        /// </summary>
        /// <value>
        /// The name of the event.
        /// </value>
        public string EventName
        {
            get { return (string)GetValue(EventNameProperty); }
            set { SetValue(EventNameProperty, value); }
        }

        /// <summary>
        /// The Command Dependency property.
        /// </summary>
        public static readonly DependencyProperty CommandProperty =
          DependencyProperty.Register("Command", typeof(ICommand), typeof(EventBinding),
          new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets the command.
        /// </summary>
        /// <value>
        /// The command.
        /// </value>
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        /// <summary>
        /// The command parameter dependency property.
        /// </summary>
        public static readonly DependencyProperty CommandParameterProperty =
          DependencyProperty.Register("CommandParameter", typeof(object), typeof(EventBinding),
          new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets the command parameter.
        /// </summary>
        /// <value>
        /// The command parameter.
        /// </value>
        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        /// <summary>
        /// Binds the specified o.
        /// </summary>
        /// <param name="o">The o.</param>
        public void Bind(object o)
        {
            try
            {

                //  Get the event info from the event name.
                EventInfo eventInfo = o.GetType().GetEvent(EventName);

                //  Get the method info for the event proxy.
                MethodInfo methodInfo = GetType().GetMethod("EventProxy", BindingFlags.NonPublic | BindingFlags.Instance);

                //  Create a delegate for the event to the event proxy.
                Delegate del = Delegate.CreateDelegate(eventInfo.EventHandlerType, this, methodInfo);

                //  Add the event handler. (Removing it first if it already exists!)
                eventInfo.RemoveEventHandler(o, del);
                eventInfo.AddEventHandler(o, del);
            }
            catch (Exception e)
            {
                string s = e.ToString();
            }
        }

        /// <summary>
        /// Proxy to actually fire the event.
        /// </summary>
        /// <param name="o">The object.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void EventProxy(object o, EventArgs e)
        {   
            if (Command != null)
                Command.Execute(CommandParameter);
        }
    }

    public static class EventBindings
    {
        /// <summary>
        /// The Event Bindings Property.
        /// </summary>
        private static readonly DependencyProperty EventBindingsProperty =
          DependencyProperty.RegisterAttached("EventBindings", typeof(EventBindingCollection), typeof(EventBindings),
          new PropertyMetadata(null, new PropertyChangedCallback(OnEventBindingsChanged)));

        /// <summary>
        /// Gets the event bindings.
        /// </summary>
        /// <param name="o">The o.</param>
        /// <returns></returns>
        public static EventBindingCollection GetEventBindings(DependencyObject o)
        {
            return (EventBindingCollection)o.GetValue(EventBindingsProperty);
        }

        /// <summary>
        /// Sets the event bindings.
        /// </summary>
        /// <param name="o">The o.</param>
        /// <param name="value">The value.</param>
        public static void SetEventBindings(DependencyObject o, EventBindingCollection value)
        {
            o.SetValue(EventBindingsProperty, value);
        }

        /// <summary>
        /// Called when event bindings changed.
        /// </summary>
        /// <param name="o">The o.</param>
        /// <param name="args">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        public static void OnEventBindingsChanged(DependencyObject o, DependencyPropertyChangedEventArgs args)
        {
            //  Cast the data.
            EventBindingCollection oldEventBindings = args.OldValue as EventBindingCollection;
            EventBindingCollection newEventBindings = args.NewValue as EventBindingCollection;

            //  If we have new set of event bindings, bind each one.
            if (newEventBindings != null)
            {
                foreach (EventBinding binding in newEventBindings)
                {
                    binding.Bind(o);
                }
            }
        }
    }

    /// <summary>
    /// Collection of event bindings.
    /// </summary>
    public class EventBindingCollection : FreezableCollection<EventBinding>
    {
    }
}
