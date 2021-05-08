using Photon;

namespace Tribot
{
    public abstract class TribotBehaviour : PunBehaviour
    {
        protected bool IsMaster
        {
            get { return (PhotonNetwork.isMasterClient || PhotonNetwork.offlineMode); }
        }

        protected bool ComparePlayerIndex(int index)
        {
            return (int)PhotonNetwork.player.CustomProperties["Index"] == index;
        }

        protected bool ComparePlayerIndex(int index, PhotonPlayer player)
        {
            return (int)player.CustomProperties["Index"] == index;
        }

        public void ToggleActive()
        {
            this.enabled = !this.enabled;
        }
    }
}


