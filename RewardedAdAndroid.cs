using System;
using System.Collections;
using Unity.Services.Core;
using Unity.Services.Mediation;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Unity.Example
{

    public class RewardedAdAndroid : MonoBehaviour
    {
        IRewardedAd ad;
        string adUnitId = "Rewarded_Android";
        string gameId = "4879133";
        PlayerMovement playerMovement;
        public TextMeshProUGUI tryAgainText;

        void Start(){
            InitServices();
            playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
            tryAgainText.enabled = false;
        }
        public async void InitServices()
        {
            try
            {
                InitializationOptions initializationOptions = new InitializationOptions();
                initializationOptions.SetGameId(gameId);
                await UnityServices.InitializeAsync(initializationOptions);

                InitializationComplete();
            }
            catch (Exception e)
            {
                InitializationFailed(e);
                tryAgainText.enabled = true;
                WaitTime();
                tryAgainText.enabled = false;
                
            }
        }

        public void SetupAd()
        {
            //Create
            ad = MediationService.Instance.CreateRewardedAd(adUnitId);

            //Subscribe to events
            ad.OnClosed += AdClosed;
            ad.OnClicked += AdClicked;
            ad.OnLoaded += AdLoaded;
            ad.OnFailedLoad += AdFailedLoad;
            ad.OnUserRewarded += UserRewarded;

            // Impression Event
            MediationService.Instance.ImpressionEventPublisher.OnImpression += ImpressionEvent;
        }
        
        public async void ShowAd()
        {
            if (ad.AdState == AdState.Loaded)
            {
                try
                {
                    RewardedAdShowOptions showOptions = new RewardedAdShowOptions();
                    showOptions.AutoReload = true;
                    await ad.ShowAsync(showOptions);
                    AdShown();
                }
                catch (ShowFailedException e)
                {
                    AdFailedShow(e);
                    tryAgainText.enabled = true;
                    WaitTime();
                    tryAgainText.enabled = false;
                }
            }
        }

        void InitializationComplete()
        {
            SetupAd();
            LoadAd();
        }

        async void LoadAd()
        {
            try
            {
                await ad.LoadAsync();
            }
            catch (LoadFailedException)
            {
                // We will handle the failure in the OnFailedLoad callback
            }
        }

        void InitializationFailed(Exception e)
        {
            Debug.Log("Initialization Failed: " + e.Message);
            tryAgainText.enabled = true;
            InitServices();
            WaitTime();
            tryAgainText.enabled = false;
            
        }

        void AdLoaded(object sender, EventArgs e)
        {
            Debug.Log("Ad loaded");
        }

        void AdFailedLoad(object sender, LoadErrorEventArgs e)
        {
            Debug.Log("Failed to load ad");
            Debug.Log(e.Message);
            tryAgainText.enabled = true;
            WaitTime();
            tryAgainText.enabled = false;
        }
        
        void AdShown()
        {
            Debug.Log("Ad shown");
        }
        
        void AdClosed(object sender, EventArgs e)
        {
            Debug.Log("Ad has closed");
            // Execute logic after an ad has been closed.
        }

        void AdClicked(object sender, EventArgs e)
        {
            Debug.Log("Ad has been clicked");
            // Execute logic after an ad has been clicked.
        }
        
        void AdFailedShow(ShowFailedException e)
        {
            Debug.Log(e.Message);
            tryAgainText.enabled = true;
            WaitTime();
            tryAgainText.enabled = false;
        }

        void ImpressionEvent(object sender, ImpressionEventArgs args)
        {
            var impressionData = args.ImpressionData != null ? JsonUtility.ToJson(args.ImpressionData, true) : "null";
            Debug.Log("Impression event from ad unit id " + args.AdUnitId + " " + impressionData);
        }
        
        void UserRewarded(object sender, RewardEventArgs e)
        {
            playerMovement.stars++;
            playerMovement.starText.text = playerMovement.stars.ToString();
            playerMovement.shopStarText.text = playerMovement.stars.ToString();
            PlayerPrefs.SetInt("stars",playerMovement.stars);
        }
        IEnumerator WaitTime()
        {
            //yield on a new YieldInstruction that waits for 5 seconds.
            yield return new WaitForSeconds(5);
        }
    }
}