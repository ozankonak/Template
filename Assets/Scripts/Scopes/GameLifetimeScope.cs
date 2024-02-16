using System.Collections.Generic;
using Cameras;
using Containers;
using Game;
using Observer;
using Player;
using Providers;
using Sheeps;
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
        [SerializeField] private GameObject SheepPrefab;

        [SerializeField] private SerializableDictionary<int, Sheep> SheepDictionary = new SerializableDictionary<int, Sheep>();

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
            builder.RegisterComponent(SheepDictionary);
            
            builder.Register<ISheepFactory, SheepFactory>(Lifetime.Transient)
                .WithParameter("sheepPrefab", SheepPrefab);
            
            builder.RegisterInstance(playerCharacterController).AsSelf(); 
            builder.RegisterInstance(playerAnimator).AsSelf();

            builder.Register<PlayerMovement>(Lifetime.Singleton).WithParameter(playerCharacterController);
            builder.Register<PlayerAnimation>(Lifetime.Singleton).WithParameter(playerAnimator);

            builder.RegisterComponentInHierarchy<PlayerCollision>();
            builder.RegisterComponentInHierarchy<SheepDestroyer>();
            
            builder.Register<VFXSystem>(Lifetime.Singleton);
            builder.Register<InputSystem>(Lifetime.Singleton);
            builder.Register<CameraFollow>(Lifetime.Singleton);
            
            builder.Register<UpdateProvider>(Lifetime.Singleton).As<ITickable, UpdateProvider>();
            builder.Register<LateUpdateProvider>(Lifetime.Singleton).As<ILateTickable, LateUpdateProvider>();
            
            //Start point of game
            builder.RegisterEntryPoint<GameStarter>();
        }
    }
}
