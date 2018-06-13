﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GAF;
using GAF.Operators;

namespace Coding_Attempt_with_GUI
{
    public class KO_Optimizer : Master_Optimizer
    {
        public KO_CornverVariables KO_CV_FL;

        public KO_CornverVariables KO_CV_FR;

        public KO_CornverVariables KO_CV_RL;

        public KO_CornverVariables KO_CV_RR;

        /// <summary>
        /// <para>---1st--- This mehod is to be called First</para>
        /// <para>The Constructor of the <see cref="SetupChange_Optimizer"/> Class</para>
        /// </summary>
        /// <param name="_crossover">Percentage of the Population which is going to be subject to Crossover</param>
        /// <param name="_mutation">Percentage of the Population which is going to be subject to Random Mutation</param>
        /// <param name="_elites">Percentage of the population which is going to be treated as Elite without an modifications to the Genes</param>
        /// <param name="_popSize">Size of the Population</param>
        /// <param name="_chromoseLength"> <para>Chromosome Length of the <see cref="Population"/></para>
        /// <para>Decided based on 2 things: The number of Genes and the bit size required for each Gene</para>
        /// <para>https://gaframework.org/wiki/index.php/How_to_Encode_Parameters for more information</para> </param>
        public KO_Optimizer(double _crossover, double _mutation, int _elites, int _noOfGenerations)
        {
            CrossOverProbability = _crossover;

            MutationProbability = _mutation;

            ElitePercentage = _elites;

            No_Generations = _noOfGenerations;

            Elites = new Elite(ElitePercentage);

            Crossovers = new Crossover(CrossOverProbability, false, CrossoverType.SinglePoint);

            Mutations = new BinaryMutate(MutationProbability, false);

            Opt_AdjToolValues = new Dictionary<string, double>();
        }

        /// <summary>
        /// <para>---2nd---To be called 2nd </para>
        /// <para>This method initializes the Objects of the <see cref="KO_CornverVariables"/> Class of each of the Corners</para>
        /// </summary>
        /// <param name="_vehicle">Object of the Vehicle class which will be used to initialize all the <see cref="KO_CornverVariables"/> Objects passed </param>
        /// <param name="_koCVFL"></param>
        /// <param name="_koCVFR"></param>
        /// <param name="_koCVRL"></param>
        /// <param name="_koCVRR"></param>
        public void Initialize_CornverVariables(Vehicle _vehicle, KO_CornverVariables _koCVFL, KO_CornverVariables _koCVFR, KO_CornverVariables _koCVRL, KO_CornverVariables _koCVRR)
        {
            Vehicle = _vehicle;

            KO_CV_FL = _koCVFL;

            KO_CV_FR = _koCVFR;

            KO_CV_RL = _koCVRL;

            KO_CV_RR = _koCVRR;
        }

    }
}