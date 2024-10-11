using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shared.Repository
{
    public class FakeRatingLocalRepository : IntRepositoryImpl
    {
        public static readonly FakeRatingLocalRepository DefaultInstance = new FakeRatingLocalRepository();

        public FakeRatingLocalRepository(int defaultValue = 0) : base("FAKE_RATING", defaultValue)
        {
        }
    }
}