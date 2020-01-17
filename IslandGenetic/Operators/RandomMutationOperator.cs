using IslandGenetic.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;

namespace IslandGenetic.Operators
{
    /// <summary>
    /// Двухточечный оператор мутации
    /// </summary>
    public class RandomMutationOperator : MutationOperator
    {
        public override IIndividual Mutate(IIndividual individual)
        {
            int chromosome1Index = Random.Next(0, individual.Genome.Count - 1);
            int chromosome2Index = -1;
            while (chromosome2Index == -1 || chromosome2Index == chromosome1Index)
            {
                chromosome2Index = Random.Next(0, individual.Genome.Count - 1);
            }

            IChromosome chromosome1 = individual.Genome[chromosome1Index];
            IChromosome chromosome2 = individual.Genome[chromosome2Index];

            BitChanger(chromosome1);
            BitChanger(chromosome2);
            return individual;
        }

        private void BitChanger(IChromosome chromosome)
        {
            BitArray bites = new BitArray(chromosome.Value.ToArray());
            int index = Random.Next(0, bites.Length - 1);
            bites[index] = !bites[index];
            chromosome.Value = ToBytes(bites);
        }

        public List<byte> ToBytes(BitArray bits, bool MSB = false)
        {
            int bitCount = 7;
            int outByte = 0;
            List<byte> bytes = new List<byte>();
            foreach (bool bitValue in bits)
            {
                if (bitValue)
                    outByte |= MSB ? 1 << bitCount : 1 << (7 - bitCount);
                if (bitCount == 0)
                {
                    bytes.Add((byte)outByte);
                    bitCount = 8;
                    outByte = 0;
                }
                bitCount--;
            }
            // Last partially decoded byte
            if (bitCount < 7)
                bytes.Add((byte)outByte);

            return bytes;
        }
    }
}