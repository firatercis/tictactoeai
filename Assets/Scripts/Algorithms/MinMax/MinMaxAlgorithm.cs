using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public interface IMinMaxState
{
    IMinMaxState[] GetSuccessors();
    string GetTransitionID();
    void SetTransitionID(string transitionID);
}

public interface IMinMaxEvaluator
{
    double Evaluate(IMinMaxState inState);
}


public class MinMaxAlgorithm
{
    public const double MAX_VAL = 1;
   
    public double GetMinMaxValue(IMinMaxState rootState, IMinMaxEvaluator evaluator, int depth, double turn, out string transitionID, double pruneValue) // turn: 1 if it is our turn, -1 if opponent's
    {
        double result = 0;
        transitionID = "end";
        if(depth <= 0) // Breaking condition for our recursive function. If the depth is 0, return the value of root.
        {
            result = evaluator.Evaluate(rootState);

        }
        else // Get successors and pass the appropriate parameters to the function itself - recursive part
        {
            IMinMaxState[] successors = rootState.GetSuccessors();
            if (successors.Length == 0)
                result = evaluator.Evaluate(rootState);
            else
            {
                double selectedBestValue = -MAX_VAL * turn; // if looking for max, start with MIN_VAL vica versa
                IMinMaxState selectedState = null;
                for (int i = 0; i < successors.Length; i++)
                {
                    string currentTransition;
                    double currentValue = GetMinMaxValue(successors[i], evaluator, depth - 1, -turn, out currentTransition, selectedBestValue);

                    if(turn == 1)
                    {


                        // Take max
                     //   if (currentValue < pruneValue) break;
                        if (currentValue > selectedBestValue || (currentValue == selectedBestValue && Random.value > 0.5))
                        {
                            selectedBestValue = currentValue; // Set the value for the best successor
                            selectedState = successors[i]; // Set the selected game state that adventageous state 
                            transitionID = selectedState.GetTransitionID(); // Store the transition ID of the current selected move
                        }
                    }
                    else
                    {
                        // Take min
                       // if (currentValue > pruneValue) break;
                        if (currentValue < selectedBestValue || ( currentValue == selectedBestValue && Random.value > 0.5))
                        {
                            selectedBestValue = currentValue; // Set the value for the best successor
                            selectedState = successors[i]; // Set the selected game state that adventageous state 
                            transitionID = selectedState.GetTransitionID(); // Store the transition ID of the current selected move
                        }
                    }

                   
                }
                result = selectedBestValue;
            }   
        }
       
        return result;

    }

}

