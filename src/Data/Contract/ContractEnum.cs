using System;

namespace Stellmart.Api.Data.Contract
{
    public enum ContractPreTnxType
    {
        Setup,
        PreTxnShipping, /* Phase 1 */
        PreTxnTimeOverride1, /* Phase 1 */
        PreTxnDelivery, /* Phase 2 */
        PreTxnTimeOverride2, /* Phase 2 */
        PreTxnAccountMerge, /* Phase 3 */
        PreTxnDispute, /* Phase 3 */
        PreTxnMergeOverride, /* Phase 3 */
        PreTxnSetWeight, /* Phase 3 - Dispute */
    }
    public enum ContractState
    {
        Initial,
        Preliminary,
        Activated,
        Completed,
        Contested
    }
    public enum ContractPhaseType
    {

        Phase0, /* Setup*/
        Phase1, /* Fund */
        Phase2, /* Shipping */
        Phase3, /* Delivery */
        Phase4Receipt, /* Receipt*/
        Phase4Dispute, /* Dispute */
        Phase5, /* Resolution*/
    }
    public enum SignatureType
    {
        User,
        System,
        SecretKey,
    }
}
