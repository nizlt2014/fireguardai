import { jwtDecode } from "jwt-decode";

export function getUserFromToken() {
  const token = localStorage.getItem("token");

  if (!token) return null;

  try {
    const decoded: any = jwtDecode(token);

    return {
      email:
        decoded[
          "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"
        ] || "",

      role:
        decoded[
          "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
        ] || "",

      id:
        decoded[
          "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"
        ] || ""
    };
  } catch {
    return null;
  }
}