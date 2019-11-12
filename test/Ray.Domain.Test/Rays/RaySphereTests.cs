﻿using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Ray.Domain.Maths.Simulations.Intersections;
using Ray.Domain.Model;
using Xunit;
using Xunit.Gherkin.Quick;

namespace Ray.Domain.Test.Rays
{
    [FeatureFile("./features/rays/RaySphere.feature")]
    public sealed class RaySphereTests : Feature
    {
        private Vector4 _origin, _direction;
        private readonly Model.Ray _rayInstance = new Model.Ray();
        private Sphere _sphereInstance = null;
        private RaySphereCalculator _xs = null;
        private float _distance;

        [Given(@"origin equals tuple (-?\d+) (-?\d+) (-?\d+) (-?\d+)")]
        public void InitializationValues_SetOnOriginInstance(float x, float y, float z, float w)
        {
            _origin.X = x;
            _origin.Y = y;
            _origin.Z = z;
            _origin.W = w;
        }

        [And(@"direction equals tuple (-?\d+) (-?\d+) (-?\d+) (-?\d+)")]
        public void InitializationValues_SetOnDirectionInstance(float x, float y, float z, float w)
        {
            _direction.X = x;
            _direction.Y = y;
            _direction.Z = z;
            _direction.W = w;
        }

        [And(@"distance equals (-?\d+\.\d+)")]
        public void InitializationValues_SetOnDistanceInstance(float t)
        {
            _distance = t;
        }

        [When(@"initialize ray with origin and direction")]
        public void InitializationValues_SetOnRayInstance()
        {
            _rayInstance.Origin = _origin;
            _rayInstance.Direction = _direction;
        }
        
        [And(@"initialize sphere as a unit sphere at the origin")]
        public void InitializationValues_SetOnSphereInstance()
        {
            _sphereInstance = new Sphere();
        }

        [And(@"initialize xs as intersection calulator for ray, sphere")]
        public void InitializationValues_SetOnIntersectionCalculator()
        {
            _xs = new RaySphereCalculator(_rayInstance, _sphereInstance);
        }

        [Then(@"ray origin equals tuple (-?\d+) (-?\d+) (-?\d+) (-?\d+)")]
        public void GivenExpectedAnswer_VerifyRayOrigin(float x, float y, float z, float w)
        {
            var expectedAnswer = new Vector4(x, y, z, w);

            var actualAnswer = _rayInstance.Origin;

            Assert.Equal(expectedAnswer, actualAnswer);
        }

        [Then(@"ray position equals tuple (-?\d+\.\d+) (-?\d+\.\d+) (-?\d+\.\d+) (-?\d+\.\d+)")]
        public void GivenExpectedAnswer_MoveAlongRay_VerifyPosition(float x, float y, float z, float w)
        {
            var expectedAnswer = new Vector4(x, y, z, w);

            var actualAnswer = _rayInstance.GetPosition(_distance);

            Assert.Equal(expectedAnswer, actualAnswer);
        }

        [And(@"ray direction equals tuple (-?\d+) (-?\d+) (-?\d+) (-?\d+)")]
        public void GivenExpectedAnswer_VerifyRayDirection(float x, float y, float z, float w)
        {
            var expectedAnswer = new Vector4(x, y, z, w);

            var actualAnswer = _rayInstance.Direction;

            Assert.Equal(expectedAnswer, actualAnswer);
        }

        [Then(@"xs intersection count equals (\d)")]
        public void GivenExpectedAnswer_RunIntersectionSimulation_VerifyCount(int count)
        {
            var expectedAnswer = count;

            _xs.RunSimulation();
            var intersections = _xs.Intersections;
            var actualAnswer = intersections.Count;

            Assert.Equal(expectedAnswer, actualAnswer);
        }

        [And(@"xs element (\d) has t equals (-?\d+\.\d+)")]
        public void GivenExpectedAnswer_QueryIntersection_VerifyDistance(int index, float t)
        {
            var expectedAnswer = t;

            var intersection = _xs.Intersections[index];
            var actualAnswer = intersection.GetPreciseIntersectionPoint().Distance;

            Assert.Equal(expectedAnswer, actualAnswer);
        }

    }
}