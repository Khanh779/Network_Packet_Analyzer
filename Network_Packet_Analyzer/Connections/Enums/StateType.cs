namespace Network_Packet_Analyzer.Connections.Enums
{
    /// <summary>
    /// Enumeration representing the various states of a TCP connection.
    /// </summary>
    public enum StateType : int
    {
        /// <summary>
        /// The connection is closed.
        /// </summary>
        CLOSED,

        /// <summary>
        /// The connection is listening for incoming connections.
        /// </summary>
        LISTEN,

        /// <summary>
        /// The connection has sent a SYN (synchronize) packet.
        /// </summary>
        SYN_SENT,

        /// <summary>
        /// The connection has received a SYN packet.
        /// </summary>
        SYN_RECEIVED,

        /// <summary>
        /// The connection is established.
        /// </summary>
        ESTABLISHED,

        /// <summary>
        /// The connection is in the first stage of closing (FIN_WAIT_1).
        /// </summary>
        FIN_WAIT_1,

        /// <summary>
        /// The connection is in the second stage of closing (FIN_WAIT_2).
        /// </summary>
        FIN_WAIT_2,

        /// <summary>
        /// The connection is waiting for a closing response (CLOSE_WAIT).
        /// </summary>
        CLOSE_WAIT,

        /// <summary>
        /// The connection is in the process of closing (CLOSING).
        /// </summary>
        CLOSING,

        /// <summary>
        /// The connection has received the last acknowledgment (LAST_ACK).
        /// </summary>
        LAST_ACK,

        /// <summary>
        /// The connection is in the TIME_WAIT state.
        /// </summary>
        TIME_WAIT,

        /// <summary>
        /// The connection is deleted (DELETE_TCB).
        /// </summary>
        DELETE_TCB,

        /// <summary>
        /// The state of the connection is unknown.
        /// </summary>
        UNKNOWN = -1
    }

}
