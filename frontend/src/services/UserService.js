export function SaveUserRoles(roles){ 
    const json = JSON.stringify(roles);
    localStorage.setItem('roles', json);

}
export function CheckUserRole(role){
    // Pobranie danych z lokalnego magazynu
    const json = localStorage.getItem('roles');
    if (!json) {
        // Jeśli nie ma żadnych ról w magazynie, zwracamy false
        return false;
    }
    
    // Parsowanie danych do tablicy ról
    const roles = JSON.parse(json);

    // Sprawdzenie czy dana rola znajduje się w tablicy ról
    return roles.includes(role);
}
