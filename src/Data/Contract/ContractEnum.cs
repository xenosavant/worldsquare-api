using System;

namespace Stellmart.Api.Data.Contract
{
    public enum ContractType
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
    public enum ContractPhase
    {
        Phase1, /* Shipping */
        Phase2, /* Delivery */
        Phase3Receipt, /* Receipt*/
        Phase3Dispute, /* Dispute */
        Phase4, /* Resolution*/
    }
}
