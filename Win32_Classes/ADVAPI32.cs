﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Win32_Classes
{
    public static class ADVAPI32
    {
        #region Structs
        public class USEROBJECTFLAGS
        {
            public int fInherit = 0;
            public int fReserved = 0;
            public int dwFlags = 0;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct SECURITY_ATTRIBUTES
        {
            public int Length;
            public IntPtr lpSecurityDescriptor;
            public bool bInheritHandle;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct PROCESS_INFORMATION
        {
            public IntPtr hProcess;
            public IntPtr hThread;
            public int dwProcessId;
            public int dwThreadId;
        }
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct STARTUPINFO
        {
            public Int32 cb;
            public string lpReserved;
            public string lpDesktop;
            public string lpTitle;
            public Int32 dwX;
            public Int32 dwY;
            public Int32 dwXSize;
            public Int32 dwYSize;
            public Int32 dwXCountChars;
            public Int32 dwYCountChars;
            public Int32 dwFillAttribute;
            public Int32 dwFlags;
            public Int16 wShowWindow;
            public Int16 cbReserved2;
            public IntPtr lpReserved2;
            public IntPtr hStdInput;
            public IntPtr hStdOutput;
            public IntPtr hStdError;
        }
        #endregion

        #region Enums
        public enum TOKEN_INFORMATION_CLASS
        {
            /// <summary>
                /// The buffer receives a TOKEN_USER structure that contains the user account of the token.
                /// </summary>
            TokenUser = 1,

            /// <summary>
                /// The buffer receives a TOKEN_GROUPS structure that contains the group accounts associated with the token.
                /// </summary>
            TokenGroups,

            /// <summary>
                /// The buffer receives a TOKEN_PRIVILEGES structure that contains the privileges of the token.
                /// </summary>
            TokenPrivileges,

            /// <summary>
                /// The buffer receives a TOKEN_OWNER structure that contains the default owner security identifier (SID) for newly created objects.
                /// </summary>
            TokenOwner,

            /// <summary>
                /// The buffer receives a TOKEN_PRIMARY_GROUP structure that contains the default primary group SID for newly created objects.
                /// </summary>
            TokenPrimaryGroup,

            /// <summary>
                /// The buffer receives a TOKEN_DEFAULT_DACL structure that contains the default DACL for newly created objects.
                /// </summary>
            TokenDefaultDacl,

            /// <summary>
                /// The buffer receives a TOKEN_SOURCE structure that contains the source of the token. TOKEN_QUERY_SOURCE access is needed to retrieve this information.
                /// </summary>
            TokenSource,

            /// <summary>
                /// The buffer receives a TOKEN_TYPE value that indicates whether the token is a primary or impersonation token.
                /// </summary>
            TokenType,

            /// <summary>
                /// The buffer receives a SECURITY_IMPERSONATION_LEVEL value that indicates the impersonation level of the token. If the access token is not an impersonation token, the function fails.
                /// </summary>
            TokenImpersonationLevel,

            /// <summary>
                /// The buffer receives a TOKEN_STATISTICS structure that contains various token statistics.
                /// </summary>
            TokenStatistics,

            /// <summary>
                /// The buffer receives a TOKEN_GROUPS structure that contains the list of restricting SIDs in a restricted token.
                /// </summary>
            TokenRestrictedSids,

            /// <summary>
                /// The buffer receives a DWORD value that indicates the Terminal Services session identifier that is associated with the token. 
                /// </summary>
            TokenSessionId,

            /// <summary>
                /// The buffer receives a TOKEN_GROUPS_AND_PRIVILEGES structure that contains the user SID, the group accounts, the restricted SIDs, and the authentication ID associated with the token.
                /// </summary>
            TokenGroupsAndPrivileges,

            /// <summary>
                /// Reserved.
                /// </summary>
            TokenSessionReference,

            /// <summary>
                /// The buffer receives a DWORD value that is nonzero if the token includes the SANDBOX_INERT flag.
                /// </summary>
            TokenSandBoxInert,

            /// <summary>
                /// Reserved.
                /// </summary>
            TokenAuditPolicy,

            /// <summary>
                /// The buffer receives a TOKEN_ORIGIN value. 
                /// </summary>
            TokenOrigin,

            /// <summary>
                /// The buffer receives a TOKEN_ELEVATION_TYPE value that specifies the elevation level of the token.
                /// </summary>
            TokenElevationType,

            /// <summary>
                /// The buffer receives a TOKEN_LINKED_TOKEN structure that contains a handle to another token that is linked to this token.
                /// </summary>
            TokenLinkedToken,

            /// <summary>
                /// The buffer receives a TOKEN_ELEVATION structure that specifies whether the token is elevated.
                /// </summary>
            TokenElevation,

            /// <summary>
                /// The buffer receives a DWORD value that is nonzero if the token has ever been filtered.
                /// </summary>
            TokenHasRestrictions,

            /// <summary>
                /// The buffer receives a TOKEN_ACCESS_INFORMATION structure that specifies security information contained in the token.
                /// </summary>
            TokenAccessInformation,

            /// <summary>
                /// The buffer receives a DWORD value that is nonzero if virtualization is allowed for the token.
                /// </summary>
            TokenVirtualizationAllowed,

            /// <summary>
                /// The buffer receives a DWORD value that is nonzero if virtualization is enabled for the token.
                /// </summary>
            TokenVirtualizationEnabled,

            /// <summary>
                /// The buffer receives a TOKEN_MANDATORY_LABEL structure that specifies the token's integrity level. 
                /// </summary>
            TokenIntegrityLevel,

            /// <summary>
                /// The buffer receives a DWORD value that is nonzero if the token has the UIAccess flag set.
                /// </summary>
            TokenUIAccess,

            /// <summary>
                /// The buffer receives a TOKEN_MANDATORY_POLICY structure that specifies the token's mandatory integrity policy.
                /// </summary>
            TokenMandatoryPolicy,

            /// <summary>
                /// The buffer receives the token's logon security identifier (SID).
                /// </summary>
            TokenLogonSid,

            /// <summary>
                /// The maximum value for this enumeration
                /// </summary>
            MaxTokenInfoClass
        }
        public enum LOGON_TYPE
        {
            LOGON32_LOGON_INTERACTIVE = 2,
            LOGON32_LOGON_NETWORK,
            LOGON32_LOGON_BATCH,
            LOGON32_LOGON_SERVICE,
            LOGON32_LOGON_UNLOCK = 7,
            LOGON32_LOGON_NETWORK_CLEARTEXT,
            LOGON32_LOGON_NEW_CREDENTIALS
        }
        public enum LOGON_PROVIDER
        {
            LOGON32_PROVIDER_DEFAULT,
            LOGON32_PROVIDER_WINNT35,
            LOGON32_PROVIDER_WINNT40,
            LOGON32_PROVIDER_WINNT50
        }
        [Flags]
        public enum CreateProcessFlags
        {
            CREATE_BREAKAWAY_FROM_JOB = 0x01000000,
            CREATE_DEFAULT_ERROR_MODE = 0x04000000,
            CREATE_NEW_CONSOLE = 0x00000010,
            CREATE_NEW_PROCESS_GROUP = 0x00000200,
            CREATE_NO_WINDOW = 0x08000000,
            CREATE_PROTECTED_PROCESS = 0x00040000,
            CREATE_PRESERVE_CODE_AUTHZ_LEVEL = 0x02000000,
            CREATE_SEPARATE_WOW_VDM = 0x00000800,
            CREATE_SHARED_WOW_VDM = 0x00001000,
            CREATE_SUSPENDED = 0x00000004,
            CREATE_UNICODE_ENVIRONMENT = 0x00000400,
            DEBUG_ONLY_THIS_PROCESS = 0x00000002,
            DEBUG_PROCESS = 0x00000001,
            DETACHED_PROCESS = 0x00000008,
            EXTENDED_STARTUPINFO_PRESENT = 0x00080000,
            INHERIT_PARENT_AFFINITY = 0x00010000
        }
        public enum TOKEN_TYPE : int
        {
            TokenPrimary = 1,
            TokenImpersonation = 2
        }

        public enum SECURITY_IMPERSONATION_LEVEL : int
        {
            SecurityAnonymous = 0,
            SecurityIdentification = 1,
            SecurityImpersonation = 2,
            SecurityDelegation = 3,
        }

        #endregion

        #region Constants
        public const int TOKEN_DUPLICATE = 0x0002;
        public const uint MAXIMUM_ALLOWED = 0x2000000;
        public const int CREATE_NEW_CONSOLE = 0x00000010;
        public const int TOKEN_ALL_ACCESS = 0x000f01ff;
        public const int PROCESS_ALL_ACCESS = STANDARD_RIGHTS_REQUIRED | SYNCHRONIZE | 0xFFF;
        public const int STANDARD_RIGHTS_REQUIRED = 0x000F0000;
        public const int SYNCHRONIZE = 0x00100000;

        public const int IDLE_PRIORITY_CLASS = 0x40;
        public const int NORMAL_PRIORITY_CLASS = 0x20;
        public const int HIGH_PRIORITY_CLASS = 0x80;
        public const int REALTIME_PRIORITY_CLASS = 0x100;
        public const UInt32 SE_PRIVILEGE_ENABLED_BY_DEFAULT = 0x00000001;
        public const UInt32 SE_PRIVILEGE_ENABLED = 0x00000002;
        public const UInt32 SE_PRIVILEGE_REMOVED = 0x00000004;
        public const UInt32 SE_PRIVILEGE_USED_FOR_ACCESS = 0x80000000;
        public const Int32 ANYSIZE_ARRAY = 1;

        public const int UOI_FLAGS = 1;
        public const int UOI_NAME = 2;
        public const int UOI_TYPE = 3;
        public const int UOI_USER_SID = 4;
        public const int UOI_HEAPSIZE = 5;
        public const int UOI_IO = 6;
        #endregion

        #region DLL Imports

        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool CreateProcessAsUser(
            IntPtr hToken,
            string lpApplicationName,
            string lpCommandLine,
            ref SECURITY_ATTRIBUTES lpProcessAttributes,
            ref SECURITY_ATTRIBUTES lpThreadAttributes,
            bool bInheritHandles,
            uint dwCreationFlags,
            IntPtr lpEnvironment,
            string lpCurrentDirectory,
            ref STARTUPINFO lpStartupInfo,
            out PROCESS_INFORMATION lpProcessInformation);

        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern bool AllocateLocallyUniqueId(out IntPtr pLuid);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        public static extern SECUR32.WinErrors LsaNtStatusToWinError(SECUR32.WinStatusCodes status);

        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern bool GetTokenInformation(
            IntPtr TokenHandle,
            SECUR32.TOKEN_INFORMATION_CLASS TokenInformationClass,
            IntPtr TokenInformation,
            uint TokenInformationLength,
            out uint ReturnLength);

        [DllImport("advapi32.dll", SetLastError = true, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool LogonUser(
            [MarshalAs(UnmanagedType.LPStr)] string pszUserName,
            [MarshalAs(UnmanagedType.LPStr)] string pszDomain,
            [MarshalAs(UnmanagedType.LPStr)] string pszPassword,
            int dwLogonType,
            int dwLogonProvider,
            out IntPtr phToken);

        [DllImport("advapi32", SetLastError = true), SuppressUnmanagedCodeSecurityAttribute]
        public static extern bool OpenProcessToken(IntPtr ProcessHandle, int DesiredAccess, ref IntPtr TokenHandle);
        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public extern static bool DuplicateTokenEx(
            IntPtr hExistingToken,
            uint dwDesiredAccess,
            ref SECURITY_ATTRIBUTES lpTokenAttributes,
            SECURITY_IMPERSONATION_LEVEL ImpersonationLevel,
            TOKEN_TYPE TokenType,
            out IntPtr phNewToken);

        [DllImport("advapi32.dll", SetLastError = false)]
        public static extern uint LsaNtStatusToWinError(uint status);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool GetUserObjectInformation(IntPtr hObj, int nIndex,
             [Out] byte[] pvInfo, uint nLength, out uint lpnLengthNeeded);
        #endregion

        public static bool OpenInteractiveProcess(string applicationName, string desktopName, out PROCESS_INFORMATION procInfo)
        {
            try
            {
                uint winlogonPid = 0;
                IntPtr hUserTokenDup = IntPtr.Zero, hPToken = IntPtr.Zero, hProcess = IntPtr.Zero;
                procInfo = new PROCESS_INFORMATION();

                // Obtain session ID for active session.
                uint dwSessionId = Kernel32.WTSGetActiveConsoleSessionId();

                // Check for RDP session.  If active, use that session ID instead.
                var rdpSessionID = GetRDPSession();
                if (rdpSessionID > 0)
                {
                    dwSessionId = rdpSessionID;
                }

                // Obtain the process ID of the winlogon process that is running within the currently active session.
                Process[] processes = Process.GetProcessesByName("winlogon");
                foreach (Process p in processes)
                {
                    if ((uint)p.SessionId == dwSessionId)
                    {
                        winlogonPid = (uint)p.Id;
                    }
                }

                // Obtain a handle to the winlogon process.
                hProcess = Kernel32.OpenProcess(MAXIMUM_ALLOWED, false, winlogonPid);

                // Obtain a handle to the access token of the winlogon process.
                if (!OpenProcessToken(hProcess, TOKEN_DUPLICATE, ref hPToken))
                {
                    Kernel32.CloseHandle(hProcess);
                    return false;
                }

                // Security attibute structure used in DuplicateTokenEx and CreateProcessAsUser.
                SECURITY_ATTRIBUTES sa = new SECURITY_ATTRIBUTES();
                sa.Length = Marshal.SizeOf(sa);

                // Copy the access token of the winlogon process; the newly created token will be a primary token.
                if (!DuplicateTokenEx(hPToken, MAXIMUM_ALLOWED, ref sa, SECURITY_IMPERSONATION_LEVEL.SecurityIdentification, TOKEN_TYPE.TokenPrimary, out hUserTokenDup))
                {
                    Kernel32.CloseHandle(hProcess);
                    Kernel32.CloseHandle(hPToken);
                    return false;
                }

                // By default, CreateProcessAsUser creates a process on a non-interactive window station, meaning
                // the window station has a desktop that is invisible and the process is incapable of receiving
                // user input. To remedy this we set the lpDesktop parameter to indicate we want to enable user 
                // interaction with the new process.
                STARTUPINFO si = new STARTUPINFO();
                si.cb = (int)Marshal.SizeOf(si);
                si.lpDesktop = @"winsta0\" + desktopName;

                // Flags that specify the priority and creation method of the process.
                uint dwCreationFlags = NORMAL_PRIORITY_CLASS | CREATE_NEW_CONSOLE;

                // Create a new process in the current user's logon session.
                bool result = CreateProcessAsUser(hUserTokenDup, null, applicationName, ref sa, ref sa, false, dwCreationFlags, IntPtr.Zero, null, ref si, out procInfo);

                // Invalidate the handles.
                Kernel32.CloseHandle(hProcess);
                Kernel32.CloseHandle(hPToken);
                Kernel32.CloseHandle(hUserTokenDup);

                return result;
            }
            catch
            {
                procInfo = new PROCESS_INFORMATION() { };
                return false;
            }
        }
        public static uint GetRDPSession()
        {
            IntPtr ppSessionInfo = IntPtr.Zero;
            Int32 count = 0;
            Int32 retval = WTSAPI32.WTSEnumerateSessions(WTSAPI32.WTS_CURRENT_SERVER_HANDLE, 0, 1, ref ppSessionInfo, ref count);
            Int32 dataSize = Marshal.SizeOf(typeof(WTSAPI32.WTS_SESSION_INFO));
            var sessList = new List<WTSAPI32.WTS_SESSION_INFO>();
            Int64 current = (int)ppSessionInfo;

            if (retval != 0)
            {
                for (int i = 0; i < count; i++)
                {
                    WTSAPI32.WTS_SESSION_INFO sessInf = (WTSAPI32.WTS_SESSION_INFO)Marshal.PtrToStructure((System.IntPtr)current, typeof(WTSAPI32.WTS_SESSION_INFO));
                    current += dataSize;
                    sessList.Add(sessInf);
                }
            }
            uint retVal = 0;
            var rdpSession = sessList.Find(ses => ses.pWinStationName.ToLower().Contains("rdp") && ses.State == 0);
            if (sessList.Exists(ses => ses.pWinStationName.ToLower().Contains("rdp") && ses.State == 0))
            {
                retVal = (uint)rdpSession.SessionID;
            }
            return retVal;
        }
    }
}
