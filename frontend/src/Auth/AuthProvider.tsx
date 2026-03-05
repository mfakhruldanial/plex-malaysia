import { ReactNode, createContext, useContext, useEffect, useState } from "react";
import Cookie from 'js-cookie';

interface AuthProviderProps {
    children: ReactNode;
}

interface AuthContextType {
    isAuthenticated: boolean;
    loading: boolean;
    setLoading: (loading: boolean) => void;
    setIsAuthenticated: (isAuthenticated: boolean) => void;
    logout: () => void;
}

const AuthContext = createContext<AuthContextType>({
    isAuthenticated: false,
    loading: false,
    setLoading: () => {},
    setIsAuthenticated: () => {},
    logout: () => {}
});

export function AuthProvider({ children }: AuthProviderProps) {
    const [isAuthenticated, setIsAuthenticated] = useState(false);
    const [loading, setLoading] = useState(true);

    const logout = () => {
        Cookie.remove('AT');
        setIsAuthenticated(false);
    };

    useEffect(() => {
        const token = Cookie.get('AT')

        if (token) {
            setIsAuthenticated(true);
        }
        else{
            setIsAuthenticated(false);
        }
    });

    return (
        <>
            <AuthContext.Provider value={{ isAuthenticated, setIsAuthenticated, loading, setLoading, logout }}>
                {children}
            </AuthContext.Provider>
        </>
    )
}

export const useAuth = () => useContext(AuthContext);