using SpacePlanets.SharedModels.GameObjects;
using SpacePlanets.SharedModels.ServerToClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SpacePlanets.SharedModels.Interface
{
    public interface IGalaxyClient
    {
        Task ReceiveShipForConsole(Ship ship);

        Task ReceiveMessage(string message);
        Task ReceiveServerTime(string message);
        Task ReceiveAccessTokenResult(GetAccessTokenResult result);
        Task ReceiveAccessTokenFromRefreshToken(GetAccessTokenResult result);

        Task ReceiveCharactersForMenu(GetCharactersForMenuResult result);

        Task ReceiveShipsForMenu(GetShipsForMenuResult result);

        Task ReceivePingResponse(PingResponse result);

        Task ReceiveCharacterForManagement(GetCharacterForManagementResult result);

        Task ReceivePlayerCameraCoordinates(GetPlayerCameraCoordinatesResult result);

        Task ReceiveMapData(GetMapDataResult result);

        Task ReceiveShipMovementConfirmation(ShipMovementConfirmation result);

        Task ReceiveError(ErrorFromServer error);

        Task ReceiveLootScanResponse(LootScanResponse result);
    }
}
