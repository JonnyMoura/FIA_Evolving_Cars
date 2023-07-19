using GeneticSharp.Domain.Fitnesses;
using GeneticSharp.Domain.Chromosomes;
using System.Threading;
using UnityEngine;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System;
using System.Linq;

namespace GeneticSharp.Runner.UnityApp.Car
{
    public class CarFitness : IFitness
    {
        public CarFitness()
        {
            ChromosomesToBeginEvaluation = new BlockingCollection<CarChromosome>();
            ChromosomesToEndEvaluation = new BlockingCollection<CarChromosome>();
        }

        public BlockingCollection<CarChromosome> ChromosomesToBeginEvaluation { get; private set; }
        public BlockingCollection<CarChromosome> ChromosomesToEndEvaluation { get; private set; }


        

        public float calcFitness(float wheels, float mass, float distance, float min_time, float MaxVelocity, int IsRoadComplete)
        {

        //algoritmo 1 de gap road
        /* 
        float fitness = 0;
            if ((mass >= 250 || mass <= 450) && MaxVelocity > 0)
            {
                fitness = mass + (distance * MaxVelocity) + (((4 * distance) / MaxVelocity) * IsRoadComplete) ;
            }
            else
            {
                fitness =  (distance * MaxVelocity) + (((2 * distance) / MaxVelocity) * IsRoadComplete);
            }
            return fitness;
        */

        //algoritmo 2 de gap road
        /* 
        float fitness = 0;
            if ((mass >= 250 || mass <= 450) && MaxVelocity > 0)
            {
                fitness = (float) Math.Pow(mass + ((distance * MaxVelocity)/(min_time + 1))+ (((4 * distance) / (MaxVelocity+1)) * IsRoadComplete), 3);
            }
            else
            {
                fitness = ((distance * MaxVelocity) / (min_time + 1)) + (((2 * distance) / (MaxVelocity+1)) * IsRoadComplete);
            }
            return fitness;
        */

        //algoritmo 3 de gap road
        /*
        float fitness = 0;
            if ((mass >= 250 || mass <= 450) && MaxVelocity > 0 && distance > 400)
            {
                fitness = (float) Math.Pow(mass + ((distance * MaxVelocity)/(min_time + 1))+ (((4 * distance) / (MaxVelocity+1)) * IsRoadComplete), 3);
            }
            else
            {
                fitness = ((distance * MaxVelocity) / (min_time + 1)) + (((2 * distance) / (MaxVelocity+1)) * IsRoadComplete);
            }
            return fitness;
        */

        //algoritmo 1 de hill road
        /* 
          float fitness = 0;
            if ((mass >= 250 || mass <= 450) && MaxVelocity > 0 && wheels>10)
            {
                fitness = (float) Math.Pow((mass + ((distance * MaxVelocity))-(5*min_time + 1))+ (((4 * distance) / (MaxVelocity+1)) * IsRoadComplete), 3);
            }
            else
            {
                fitness = (((distance * MaxVelocity))-(2*min_time + 1))+ (((2* distance) / (MaxVelocity+1)) * IsRoadComplete);
            }
            return fitness;

           */

        //algoritmo 2 de hill road
        /* 
          float fitness = 0;
            if ((mass >= 250 || mass <= 450) && MaxVelocity > 0 && wheels>10)
            {
                fitness = (float) Math.Pow((mass + ((distance * MaxVelocity))-(5*min_time + 1))+ (((4 * distance) / (MaxVelocity+1)) * IsRoadComplete), 3);
            }
            else
            {
                fitness = (((distance * MaxVelocity))-(2*min_time + 1))+ (((2* distance) / (MaxVelocity+1)) * IsRoadComplete);
            }
            return fitness;

           */

            //algoritmo de obstacle road
            /*
           float fitness = 0;
            if ((mass>=200 || mass<=350) && wheels>=12 && MaxVelocity > 0 )
            {
                fitness = (float)Math.Pow((((mass*wheels*5) * ((MaxVelocity+1)*distance * 12)) + ((10 * (MaxVelocity+1) * distance)) + (distance* 15 * IsRoadComplete)),3);
            }
            else
            {
                fitness = (mass*distance*6) + (3*(MaxVelocity+1)) + (distance * 3 * IsRoadComplete);
            }
            return fitness;
            */

        }


            public double Evaluate(IChromosome chromosome)
        {
            var c = chromosome as CarChromosome;
            ChromosomesToBeginEvaluation.Add(c);

            float fitness = 0; 
            do
            {
                Thread.Sleep(1000);




                /*YOUR CODE HERE: You should define de fitness function here!!
                 * 
                 * 
                 * You have access to the following information regarding how the car performed in the scenario:
                 * MaxDistance: Maximum distance reached by the car;
                 * MaxDistanceTime: Time taken to reach the MaxDistance;
                 * MaxVelocity: Maximum Velocity reached by the car;
                 * NumberOfWheels: Number of wheels that the cars has;
                 * CarMass: Weight of the car;
                 * IsRoadComplete: This variable has the value 1 if the car reaches the end of the road, 0 otherwise.
                 * 
                */
                float MaxDistance = c.MaxDistance;
                float MaxDistanceTime = c.MaxDistanceTime;
                float MaxVelocity = c.MaxVelocity;
                float NumberOfWheels = c.NumberOfWheels;
                float CarMass = c.CarMass;
                int IsRoadComplete = c.IsRoadComplete ? 1 : 0;


                

                
                fitness = calcFitness(NumberOfWheels,CarMass,MaxDistance,MaxDistanceTime,MaxVelocity,IsRoadComplete);


                c.Fitness = fitness;

            } while (!c.Evaluated);

            ChromosomesToEndEvaluation.Add(c);

            do
            {
                Thread.Sleep(1000);
            } while (!c.Evaluated);


            return fitness;
        }

    }
}