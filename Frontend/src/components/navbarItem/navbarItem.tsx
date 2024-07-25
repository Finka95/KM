import { IconButton, Box, Typography } from "@mui/material"
import { FC, ReactNode } from "react";
import { Link as RouterLink } from "react-router-dom";

interface ItemPros {
  text?: string,
  icon: ReactNode,
  to: string
}

const NavbarItem: FC<ItemPros> = ({text, icon, to}) => {
  return (
    <IconButton component={RouterLink} to={to} sx = {{color: "#000000"}}>
      <Box 
        sx={{
          display: 'flex',
          flexDirection: 'column',
          alignItems: 'center'
        }}>
        {icon}
        <Typography
          sx={{
            fontSize: "12px"
          }}>
          {text}
            </Typography>
      </Box>
    </IconButton>
  )
}

export default NavbarItem;