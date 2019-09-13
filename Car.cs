using System;
using System.Collections.Generic;
using System.Text;

namespace CarDelegate
{
    public class Car
    {
        // Internal state data.
        public int CurrentSpeed { get; set; }
        public int MaxSpeed { get; set; }
        public string PetName { get; set; }

        // Is the car alive or dead?
        private bool carIsDead;

        // Class constructors.
        public Car() { }
        public Car(string name, int maxSp, int currSp)
        {
            CurrentSpeed = currSp;
            MaxSpeed = maxSp;
            PetName = name;
        }

        // 1) Define a delegate type.
        public delegate void CarEngineHandler(string msgForCaller);

        // 2) Define a member variable of this delegate.
        private CarEngineHandler listOfHandlers;

        // 3) Add registration function for the caller.
        // Now with multicasting support!
        // Note we are using the += operator, not the assignment operator (=).
        public void RegisterWithCarEngine(CarEngineHandler methodToCall)
        {
            listOfHandlers += methodToCall;
        }

        // 4) Implement the Accelerate() method to invoke the delegate's invocation list under the correct circumstances.
        public void Accelerate(int delta)
        {
            // If the car is "dead", send dead message.
            if (carIsDead)
            {
                listOfHandlers?.Invoke("Sorry, this car is dead...");
            }
            else
            {
                CurrentSpeed += delta;

                // Is this car "almost dead"?
                if (10 == (MaxSpeed - CurrentSpeed) && listOfHandlers != null)
                {
                    listOfHandlers("Careful buddy! Gonna blow!");
                }
                if (CurrentSpeed >= MaxSpeed)
                    carIsDead = true;
                else
                    Console.WriteLine("CurrentSpeed = {0}", CurrentSpeed);
            }
        }

        public void UnRegisterWithCarEngine(CarEngineHandler methodToCall)
        {
            listOfHandlers -= methodToCall;
        }
    }
}
