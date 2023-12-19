import { Container, Text, Button, Group, Center } from "@mantine/core";
import React from "react";
import classes from "./Hero.module.css";

export default function Hero() {
  return (
    <div className={classes.wrapper}>
      <Container size={700} className={classes.inner}>
        <Center>
          <h1 className={classes.title}>
            <Text
              component="span"
              variant="gradient"
              gradient={{ from: "blue", to: "cyan" }}
              inherit
            >
              Find your perfect sanctuary
            </Text>{" "}
          </h1>
        </Center>

        <Text className={classes.description}>
          A place to find your new home
        </Text>

        <Group className={classes.controls}>
          <Button
            component="a"
            href="/listings"
            size="xl"
            className={classes.control}
            variant="gradient"
            gradient={{ from: "blue", to: "cyan" }}
          >
            View Listings
          </Button>
        </Group>
      </Container>
    </div>
  );
}
