using System.Collections.Generic;
using Cameras;
using Containers;
using Game;
using Player;
using Providers;
using Systems;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Scopes
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private ParticleContainer particleContainer;
        [Header("Player Components")] [Space]
        [SerializeField] private CharacterController playerCharacterController;
        [SerializeField] private Animator playerAnimator;

        private Camera mainCamera;
        private Transform playerTransform;

        private List<BoxCollider> houseTriggerPoints = new List<BoxCollider>();
        private readonly string TriggerPointName = "TriggerPoint";
        
        protected override void Configure(IContainerBuilder builder)
        {
            mainCamera = Camera.main;
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

            foreach (BoxCollider bc in FindObjectsOfType<BoxCollider>())
            {
                if (bc.name.Equals(TriggerPointName)) houseTriggerPoints.Add(bc);
            }
            
            builder.RegisterComponent(mainCamera);
            builder.RegisterComponent(particleContainer);
            builder.RegisterComponent(playerTransform);
            builder.RegisterComponent(houseTriggerPoints);
            
            builder.RegisterInstance(playerCharacterController).AsSelf(); 
            builder.RegisterInstance(playerAnimator).AsSelf();

            builder.Register<PlayerMovement>(Lifetime.Singleton).WithParameter(playerCharacterController);
            builder.Register<PlayerAnimation>(Lifetime.Singleton).WithParameter(playerAnimator);

            builder.RegisterComponentInHierarchy<PlayerTrigger>();
            
            builder.Register<VFXSystem>(Lifetime.Singleton);
            builder.Register<InputSystem>(Lifetime.Singleton);
            builder.Register<CameraFollow>(Lifetime.Singleton);
            
            builder.Register<UpdateProvider>(Lifetime.Singleton).As<ITickable, UpdateProvider>();
            
            //Start point of game
            builder.RegisterEntryPoint<GameStarter>();
        }
    }
}
